public class InventorySlot : Slot
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void SlotClicked()
    {
        var previousItem = GetItem();
        base.SlotClicked();
        var inventory = gameObject.transform.root.gameObject.GetComponent<InventoryController>();
        inventory.SlotClicked(this, previousItem);
    }
}