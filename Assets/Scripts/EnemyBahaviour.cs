using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class EnemyBahaviour : ShootingEntity
{
    public abstract float MaxDistance { get; }
    public abstract float IdleTime { get;}

    private float _moveTime;

    private Vector3 _hidePosition;

    public GameObject player;

    private NavMeshAgent _thisAgent;

    public Animator enemyAnimator;

    private EnemyState _enemyConfig;


    public bool isDead;
    public void Awake()
    {
        shootSpeed = 2;
        shootDamage = 2;

        isDead = false;
        currentState = State.Attack;

        _enemyConfig = GetComponent<EnemyState>();
        _moveTime = MaxDistance / _enemyConfig.speed;

        _thisAgent = GetComponent<NavMeshAgent>();
        _thisAgent.isStopped = true;
        _thisAgent.stoppingDistance = MaxDistance;
        _thisAgent.speed = _enemyConfig.speed;

        player = SpawnEnemies.player;

        StartCoroutine("MoveToBetterPosition");
    }

    private void FixedUpdate()
    {

        if (!isDead)
        {
            if (_thisAgent.isStopped == true)
                RotateEntity(Direction());

            switch (currentState)
            {
                case State.Attack:

                    _thisAgent.SetDestination(player.transform.position);
                    break;

                case State.Hide:

                    _thisAgent.SetDestination(_hidePosition - transform.position);
                    break;
            }
        }


    }

    public IEnumerator MoveToBetterPosition()
    {
        if (!isDead)
        {
            yield return new WaitForSeconds(IdleTime);
            _thisAgent.isStopped = false;
            StopShooting(enemyAnimator);
            FindHidePosition();
            RotateEntity(Direction());
            StartCoroutine("Stay");
        }
    }

    public IEnumerator Stay()
    {
        if (!isDead)
        {
            yield return new WaitForSeconds(_moveTime);
            _thisAgent.isStopped = true;
            if (currentState == State.Idle || isObstacles == true)
            {
                enemyAnimator.SetBool("IsRunning", false);
                enemyAnimator.SetBool("IsShooting", false);
            }
            else if (isObstacles == false)
                StartShooting(enemyAnimator);
            StartCoroutine("MoveToBetterPosition");
        }
    }


    public Vector3 Direction()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 fleeDirection = (directionToPlayer).normalized;
        return fleeDirection;
    }

    public void RotateEntity(Vector3 rotateDirection)
    {
        Quaternion Angle = Quaternion.LookRotation(rotateDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Angle, 10000 * Time.deltaTime);
    }

    public void FindHidePosition()
    {
        float x = Random.Range(-1, 1);
        _hidePosition = 5 * new Vector3(x, 0, 1);
    }


}




