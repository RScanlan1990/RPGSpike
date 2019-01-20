using UnityEditor;
using UnityEngine;

public class Target : Stats
{
    [SerializeField] private float _interactRange;
    [SerializeField] private GameObject _rayCastPoint;
    private GameObject _target;
    private Targetable _targetable;


    public void TrySetTarget(GameObject target, Camera cameraToFace)
    {
        var targetable = target.GetComponent<Targetable>();
        if (targetable != null)
        {
            if (!TargetIdDifferent(targetable.TargetableId))
            {
                SetTarget(target, targetable, cameraToFace);
                return;
            }
            ChangeTarget(target, targetable, cameraToFace);
            return;
        }
        ClearTarget();               
    }

    public bool HaveTarget()
    {
        return _target != null;
    }

    protected Vector3 GetTargetPosition()
    {
        return _target.transform.position;
    }

    public float GetTargetHealth()
    {
        var k = _targetable as Killable;
        return k.GetHealth();
    }

    public Vector3 GetTargetCameraOffSet()
    {
        var i = _targetable as Interactable;
        return i.TargetCameraOffset;
    }

    public Vector3 GetTargetCameraRotation()
    {
        var i = _targetable as Interactable;
        return i.TargetCameraRotation;
    }

    public Transform GetTargetTransform()
    {
        return _target.transform;
    }

    public bool CanAttack()
    {
        return TargetIsKillable() && TargetWithinAttackRange();
    }

    public bool TargetIsKillable()
    {
        var k = _targetable as Killable;
        return k != null;
    }

    protected bool TargetWithinInteractRange()
    {
        return GetDistanceToTarget() < _interactRange;
    }

    private float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, _target.transform.position);
    }

    protected bool TargetWithinAttackRange()
    {
        return GetDistanceToTarget() < AttackRange;
    }

    protected bool TargetInFront()
    {
        var targetDir = _target.transform.position - transform.position;
        var angle = Vector3.Angle(targetDir, transform.forward);
        return angle <= 5.0f;
    }

    protected void ClearTarget()
    {
        _target = null;
        if (_targetable != null)
        {
            _targetable.GetUnTargeted();
        }
    }

    private void ChangeTarget(GameObject target, Targetable targetable, Camera cameraToFace)
    {
        ClearTarget();
        SetTarget(target, targetable, cameraToFace);
    }

    private void SetTarget(GameObject target, Targetable targetable, Camera cameraToFace)
    {
        _target = target;
        _targetable = targetable;
        _targetable.GetTargeted(cameraToFace);
    }

    public bool CanInteract()
    {
        return TargetWithinInteractRange();
    }

    protected bool CanSeeTarget()
    {
        var origin = _rayCastPoint != null ? _rayCastPoint.transform.position : transform.position;
        var ray = new Ray(origin, transform.position - GetTargetPosition());
        ray.direction = -ray.direction;
        Physics.Raycast(ray, out var hit);
        if (hit.transform != null)
        {
            if (hit.distance <= 25.0f)
            {
                return true;
            }
        }
        return false;
    }

    public bool TargetIdDifferent(GUID targetId)
    {
        if (_targetable == null)
        {
            return true;

        }
        return _targetable.TargetableId != targetId;
    }
}