using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour, IEntity
{
    public float HP { get; set; }

    public float maxHP;

    private Vector2 _levelScale;
    public GameObject hpLevel;
    public GameObject canvas;
    public GameObject thisEntity;
    public GameObject parentObject;

    protected virtual void Awake()
    {
        _levelScale = hpLevel.transform.localScale;
        HP = maxHP;
    }
    protected void Update()
    {
      canvas.transform.position = thisEntity.transform.position;
      _levelScale = new Vector3(HP / maxHP, _levelScale.y);
      hpLevel.transform.localScale = _levelScale;
        if(HP <= 0)
        {
            HP = 0;
            thisEntity.GetComponent<Animator>().SetTrigger("Death");
            Invoke("Death", 3);
            EnemyBahaviour enemy = thisEntity.GetComponent<EnemyBahaviour>();
            if (enemy != null)
                enemy.isDead = true;

        }
    }

    public virtual void Death()

    {
        Instantiate(LevelManager.coinStaticPrefab, thisEntity.transform.position + Vector3.up, Quaternion.identity);
        Destroy(parentObject);
    }

}
