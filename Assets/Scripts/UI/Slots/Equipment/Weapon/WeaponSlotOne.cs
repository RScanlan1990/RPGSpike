public class WeaponSlotOne : EquipmentSlot
{
    protected override void SlotClicked()
    {
        var equipment = gameObject.transform.root.gameObject.GetComponent<EquipedController>();
        equipment.TryEquipWeaponOne(this);
    }
}
