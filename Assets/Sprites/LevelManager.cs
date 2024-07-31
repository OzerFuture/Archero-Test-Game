using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private int _remainingEnemies;
    public float count;
    private float nextUpdate;
    public static bool levelComplete;
    public TMP_Text countText;
    public GameObject door;
    public GameObject coinPrefab;
    public static GameObject coinStaticPrefab;

    void Awake()
    {
        count = 3;
        Invoke("StartGame", 3);
        coinStaticPrefab = coinPrefab;
        levelComplete = false;
        door.SetActive(true);
    }

    private void Update()
    {
        _remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (_remainingEnemies <= 0)
        {
            LevelCompleted();
        }

        countText.text = count.ToString();

        if (Time.time >= nextUpdate && count >= 0)
        {
            nextUpdate = Time.time + 1.5f;
            count--;
        }

        if (count == 0)
        {
            countText.gameObject.SetActive(false);
        }

    }

    private void LevelCompleted()
    {
        levelComplete = true;
        door.GetComponent<MeshRenderer>().enabled = false;
        door.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void StartGame()
    {
        SpawnEnemies.player.GetComponent<PlayerController>().enabled = true;
    }

   

}
