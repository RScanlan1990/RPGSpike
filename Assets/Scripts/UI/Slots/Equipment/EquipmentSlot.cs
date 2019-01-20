using UnityEngine;

public class EquipmentSlot : Slot
{
    public Item.ItemType Type;
    public GameObject Holder;
    private GameObject _itemGraphics;

    protected override void SlotClicked()
    {
        var equipment = gameObject.transform.root.gameObject.GetComponent<EquipedController>();
        equipment.TryEquip(this);
    }

    public override void AddItem(Item item)
    {
        base.AddItem(item);
        _itemGraphics = Instantiate(item.Graphics, Holder.transform.position + item.HandOffset, Holder.transform.rotation * Quaternion.Euler(item.HandRotationOffset), Holder.transform);
        _itemGraphics.GetComponent<Collider>().enabled = false;
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        Destroy(_itemGraphics);
    }
}