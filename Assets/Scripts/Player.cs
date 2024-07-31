using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : EntityState, IMove
{
    [field: SerializeField, Range(0, 10)] public float speed { get; set; }

    private float nextUpdate;

    public GameObject winUI;

    public GameObject loseUI;

    public GameObject pause;

    protected override void Awake()
    {
        maxHP = 100;
        base.Awake();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Time.time + 2;
                HP -= 10;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Exit"))
        {
            pause.SetActive(false);
            winUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public override void Death()

    {
        pause.SetActive(false);
        loseUI.SetActive(true);
        Time.timeScale = 0;
    }

}
