using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationController))]
public class Killable : Interactable
{
    public int MaxHealth;
    public bool InCombat;
    private bool _amDead;
    private float _currentHealth;
    private float _combatTimer;
    private AnimationController _animation;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _combatTimer = 25.0f;
        _currentHealth = MaxHealth;
        _amDead = false;
        _animation = GetComponent<AnimationController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if(InCombat)
        {
            _combatTimer -= Time.fixedDeltaTime;
        }

        if(_combatTimer <= 0.0f)
        {
            _combatTimer = 25.0f;
            InCombat = false;
        }
    }

    protected bool AmDead()
    {
        if (_amDead)
        {
            return true;
        }
        if (_currentHealth <= 0)
        {
            _amDead = true;
            TryDie();
            return true;
        }

        return false;
    }

    protected virtual void TryDie()
    {
        _agent.isStopped = true;
        _animation.Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _combatTimer = 25.0f;
        InCombat = true;
        _currentHealth -= damage;
    }

    public float GetHealth()
    {
        return Mathf.Clamp(_currentHealth / MaxHealth, 0.0f, 100.0f);
    }
}
