using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEntity : MonoBehaviour, IShoot
{
      public float shootDamage { get; set; }
      public float shootSpeed { get;  set; }

      protected IEntity thisEntity;

      public GameObject fireBall;

      public Transform fireOrigin;

    public bool isObstacles;
    public enum State
    {
        Idle,
        Shooting,
        Hide,
        Moving,
        Attack
    }

    public State currentState;

    private void Update()
    {

        Ray shootDirection = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(shootDirection, out hit, 4))

        {
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                isObstacles = true;
            }
            else
                isObstacles = false;
        }

        else
            isObstacles = false;

    }


    public void StartShooting(Animator thisAnimator)
    {
        thisAnimator.SetFloat("AttackSpeed", shootSpeed);
        thisAnimator.SetBool("IsRunning", false);
        thisAnimator.SetBool("IsShooting", true);
    }

    public void Shoot()

    {
        GameObject newShootBall = Instantiate(fireBall, fireOrigin.position, Quaternion.identity);
        newShootBall.GetComponent<Rigidbody>().velocity = 5*transform.forward;
        newShootBall.GetComponent<FireBall>().CreateShootBall(gameObject, shootDamage);

    }

    public void StopShooting(Animator thisAnimator)
    {
        thisAnimator.SetBool("IsRunning", true);
        thisAnimator.SetBool("IsShooting", false);
        thisAnimator.SetBool("SplashAttack", false);
    }


}

