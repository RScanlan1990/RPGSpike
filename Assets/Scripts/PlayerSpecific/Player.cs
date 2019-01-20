using UnityEngine;

[RequireComponent(typeof(InventoryController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(Interact))]
[RequireComponent(typeof(PlayerUiController))]
[RequireComponent(typeof(EquipedController))]
[RequireComponent(typeof(CombatController))]
public class Player : Skills
{
    public LayerMask MouseLayersToHit;
    private CameraController _camera;
    private Interact _interact;
    private MovementController _movement;
    private CombatController _combatController;
    private EquipedController _equipedController;
    private RayCastWrapper _rayCastWrapper;

    protected override void Start()
    {
        base.Start();
        _camera = GetComponent<CameraController>();
        _interact = GetComponent<Interact>();
        _movement = GetComponent<MovementController>();
        _combatController = GetComponent<CombatController>();
        _equipedController = GetComponent<EquipedController>();
        _rayCastWrapper = new RayCastWrapper(_camera.GetCamera());
    }

    protected override void Update()
    {
        base.Update();
        var input = -1;
        if (Input.GetMouseButton(0))
        {
            input = 0;
        }
        else
        if (Input.GetMouseButton(1))
        {
            input = 1;
        }

        var hit = _rayCastWrapper.CastRayFromCamera(MouseLayersToHit);
        if(hit.transform == null) { return; }
        TrySetTarget(hit.transform.gameObject, _camera.GetCamera()); 
        if (input >= 0)
        {
           
            if (HaveTarget())
            {
                if (!TargetInFront())
                {
                    _movement.LookAtPosition(GetTargetPosition());
                    return;
                }

                if (CanAttack())
                {
                    _combatController.TryAttackEnemy(GetTargetTransform(),  GetSkillDamage(input) + GetStatsDamage() + _equipedController.GetWeaponDamage());
                    return;
                }

                if(CanInteract())
                {
                    _interact.TryInteract(GetTargetTransform().gameObject);
                    return;
                }
            }
            _movement.Move(hit.point);
        }
    }
}