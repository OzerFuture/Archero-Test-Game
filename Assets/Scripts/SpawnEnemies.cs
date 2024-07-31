using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private int enemiesCount = 4;
    public Collider spawnArea;
    private List<Vector3> _spawnPositions = new List<Vector3>();
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject playerPrefab;
    public static GameObject player;
    public GameObject flyingEnemy;
    public GameObject walkingEnemy;
    private void Start()
    {
        player = playerPrefab;
        Bounds spawnBounds = spawnArea.bounds;

        for (int i = 0; i < enemiesCount; i++)
        {
            Vector3 randomPosition = GetRandomPositionInArea(spawnBounds);
            _spawnPositions.Add(randomPosition);
            GameObject enemy;
            if (Random.value >= 0.5)
                enemy = Instantiate(flyingEnemy, randomPosition, Quaternion.identity);
            else
                enemy = Instantiate(walkingEnemy, randomPosition , Quaternion.identity);
            enemies.Add(enemy);
        }

    }

    private Vector3 GetRandomPositionInArea(Bounds bounds)
    {
        Vector3 randomPosition = Vector3.zero;
        bool positionIsValid = false;
        int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            float boost = 0.2f;
            float x = Random.Range(bounds.min.x + 2, bounds.max.x - 2);
            float y = spawnArea.transform.position.y + boost;
            float z = Random.Range(bounds.min.z + 8, bounds.max.z);
            randomPosition = new Vector3(x, y, z);

            positionIsValid = IsPositionValid(randomPosition);

            if (positionIsValid)
                return randomPosition;
        }

        Debug.LogWarning("spawn1");
        return randomPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {

        foreach (Vector3 existingPosition in _spawnPositions)
        {
            if (Vector3.Distance(position, existingPosition) < 2f)
            {
                return false;
            }
        }

        return true;
    }

}
