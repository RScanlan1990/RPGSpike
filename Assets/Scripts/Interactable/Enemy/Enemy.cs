using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CombatController))]
[RequireComponent(typeof(EnemyUiController))]
[RequireComponent(typeof(AnimationController))]
public class Enemy : Skills
{
    public float RunSpeed;
    public float WalkSpeed;
    public int Id;
    private CombatController _combatController;
    private NavMeshAgent _navMeshAgent;
    private bool _idle;
    private Transform _spawnPoint;
    private Vector3 _spawnPointArea;

    protected override void Start()
    {
        base.Start();
        _combatController = GetComponent<CombatController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _idle = false;
        _spawnPoint = transform.parent.transform;
        _spawnPointArea = _spawnPoint.GetComponent<BoxCollider>().size;
        _navMeshAgent.speed = WalkSpeed;
    }

    protected override void Update()
    {
        base.Update();
        if(AmDead()) { return; }
        if (HaveTarget())
        {
            if (CanSeeTarget())
            {
                if (TargetWithinAttackRange())
                {
                    if (TargetInFront())
                    {
                        Attack();
                        return;
                    }
                    LookAtTarget();
                    return;
                }
                TryMoveToTarget();
                return;
            }
            ClearTarget();
        } else if (!HaveTarget())
        {
            TryIdle();
        }  
    }

    protected override void TryDie()
    {
        _spawnPoint.GetComponent<EnemySpawn>().EnemyDied();
        base.TryDie();
    }

    private void Attack()
    {
        _idle = false;
        if (_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.isStopped = true;
        }
        _combatController.TryAttackEnemy(GetTargetTransform(), GetSkillDamage(0) + GetStatsDamage());
    }

    private void LookAtTarget()
    {
        transform.LookAt(GetTargetPosition());
    }

    private void TryMoveToTarget()
    {
        NavMesh.SamplePosition(GetTargetPosition(), out var navMeshHit, 5.0f, NavMesh.AllAreas);
        if (!navMeshHit.hit) { return; }
        _navMeshAgent.speed = RunSpeed;
        MoveToPosition(navMeshHit.position);
    }

    private void MoveToPosition(Vector3 position)
    {
        if(_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = position;
        }
    }

    private void TryIdle()
    {
        if(_idle) { return; }
        _navMeshAgent.speed = WalkSpeed;
        StartCoroutine("Idle");
    }

    private IEnumerator Idle()
    {
        _idle = true;
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        Patrol();
    }

    private void Patrol()
    {
        var navMeshHelper = new NavMeshHelper();
        var randomPosition = navMeshHelper.GetRandomPositionWithArea(_spawnPoint.transform.position, new Vector2(_spawnPointArea.x / 2, _spawnPointArea.z / 2));
        if (randomPosition == Vector3.zero) { return; }
        MoveToPosition(randomPosition);
        _idle = false;
    }
}
