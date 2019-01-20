using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _previous;
    private float _velocity;

    private Killable _killable;
    

    public void Start()
    {
        _killable = GetComponent<Killable>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _velocity = (transform.position - _previous).magnitude / Time.deltaTime;
        _previous = transform.position;
        _animator.SetFloat("Speed", _velocity);
        _animator.SetBool("InCombat", _killable.InCombat);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        _animator.SetTrigger("Attack2");
    }

    public bool Attacking()
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack");
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }
}
