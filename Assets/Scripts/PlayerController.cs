using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShootingEntity
{
    public float shootRadius = 7f;
    private Vector3 _direction;
    public VariableJoystick variableJoystick;
    private PlayerState _player;
    public Animator playerAnimator;
    public SpawnEnemies spawnEnemies;
    private EnemyBahaviour _enemy;

    private void Awake()
    {
        _player = GetComponent<PlayerState>();
        shootSpeed = 2;
        shootDamage = 10;
    }


    public void FixedUpdate()
    {
        _direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        NearestEnemy();

        if (_direction.magnitude != 0)
        {
            currentState = State.Moving;
        }

        else
        {
            if (NearestEnemy() != null)
            {
                if (Vector3.Distance(transform.position, NearestEnemy().transform.position) < shootRadius)
                {
                    Vector3 directionToNearestEnemy = NearestEnemy().transform.position - transform.position;
                    RotatePlayer(directionToNearestEnemy);

                    if (isObstacles == true)
                    {
                        currentState = State.Idle;
                    }

                    else
                        currentState = State.Shooting;
                }

                else
                    currentState = State.Idle;

            }
            else
            {
                currentState = State.Idle;
            }
        }

        if (NearestEnemy() != null)
            _enemy = NearestEnemy().GetComponentInChildren<EnemyBahaviour>();

        switch (currentState)
            {
                case State.Idle:
                    RotatePlayer(transform.forward);
                    playerAnimator.SetBool("IsRunning", false);
                    playerAnimator.SetBool("IsShooting", false);
                if (NearestEnemy() != null)
                    _enemy.currentState = EnemyBahaviour.State.Attack;
                    break;

                case State.Moving:
                    MovePlayer();
                if (NearestEnemy() != null)
                    _enemy.currentState = EnemyBahaviour.State.Attack;
                    break;

                case State.Shooting:
                    StartShooting(playerAnimator);
                if (NearestEnemy() != null)
                {
                    _enemy.currentState = EnemyBahaviour.State.Hide;
                    SplashAttackCheck();
                }
                    break;
            }

    }

    public void SplashAttackCheck()
    {
        if (Vector3.Distance(transform.position, NearestEnemy().transform.position) < 3)
        {
            playerAnimator.SetBool("SplashAttack", true);
        }
        else
            playerAnimator.SetBool("SplashAttack", false);
    }    


    public GameObject NearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
        }

        return nearestEnemy;
    }

    private void MovePlayer()
    {
        transform.position += _player.speed * 0.05f * _direction;
        transform.rotation = Quaternion.LookRotation(_direction);
        StopShooting(playerAnimator);
    }

    private void RotatePlayer(Vector3 rotateDirection)
    {
        Quaternion Angle = Quaternion.LookRotation(rotateDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Angle, 1000 * Time.deltaTime);

    }

    public void SplashAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = 3;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                enemy.GetComponent<IEntity>().HP -= shootDamage;
            }
        }

    }    

}
