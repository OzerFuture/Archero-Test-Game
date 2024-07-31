using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float shootDamage;

    private string _parentTag;

    public ParticleSystem explode;

    public GameObject fire;

    private GameObject _parent;

    private void Awake()
    {
        StartCoroutine(nameof(DestroyBall), 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("FireBall") && !other.gameObject.CompareTag("Coin"))
        {
            Debug.Log(other.gameObject.tag);
            IEntity isEntity = other.gameObject.GetComponent<IEntity>();

            if (isEntity != null && other.gameObject.tag != _parentTag)
            {
                DamageEnemy(isEntity);
                explode.Play();
            }

            StartCoroutine("DestroyBall", 0.1f);
        }
    }

    public void DamageEnemy(IEntity entity)
    {
        entity.HP -= shootDamage;
    }

    public IEnumerator DestroyBall(float time)
    {
        yield return new WaitForSeconds(time);
        fire.SetActive(false);
        Destroy(gameObject);
    }

    public void CreateShootBall(GameObject parentObject, float damage)
    {
        _parent = parentObject;
        shootDamage = damage;
        _parentTag = _parent.gameObject.tag;
    }  
    

}
