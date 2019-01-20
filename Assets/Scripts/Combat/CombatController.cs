using UnityEngine;
using UnityEngine.AI;

public class CombatController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private AnimationController _animation;
    private bool _haveAttacked;
    private float _damage;
    private Killable _target;

    private Killable _myKillable;

    protected virtual void Start()
    {
        
        _agent = GetComponent<NavMeshAgent>();
        _animation = GetComponent<AnimationController>();
        _myKillable = GetComponent<Killable>();
        _haveAttacked = false;
    }

    public void TryAttackEnemy(Transform target, float damage)
    {
        if (!_haveAttacked && !_animation.Attacking())
        {
            if (_agent.isOnNavMesh)
            {
                _agent.isStopped = true;
            }
            _myKillable.InCombat = true;
            _haveAttacked = true;
            var killable = target.GetComponent<Killable>();
            _target = killable;
            _damage = damage;
            _animation.Attack();
        }   
    }

    //Called By Animation Event!
    public void AttackEnemy()
    {
        _target.TakeDamage(_damage);
        _haveAttacked = false;
        _agent.isStopped = false;
    }
}
