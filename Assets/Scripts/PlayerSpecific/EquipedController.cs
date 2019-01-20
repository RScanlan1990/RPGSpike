using UnityEngine;

public class EquipedController : MonoBehaviour
{
    private InventoryController _inventoryController;
    private PlayerUiController _playerUiController;

    private Weapon WeaponOne;

    private void Start()
    {
        _inventoryController = GetComponent<InventoryController>();
        _playerUiController = GetComponent<PlayerUiController>();
    }

    public void TryEquipWeaponOne(EquipmentSlot slot)
    {
        if (TryEquip(slot))
        {
            WeaponOne = (Weapon)slot.GetItem();
        }
    }

    public bool TryEquip(EquipmentSlot slot)
    {
        var selectedItem = _inventoryController.GetSelectedItem();
        if(selectedItem != null)
        {
            if (slot.Type == selectedItem.Type)
            {
                Equip(slot, selectedItem, slot.GetItem());
                return true;
            }     
        } 
        DeEquip(slot);
        return false;
    }

    private void Equip(EquipmentSlot slot, Item selectedItem, Item previousItem)
    {
        _playerUiController.SetMouseIcon(previousItem == null ? null : previousItem.Image);
        slot.AddItem(selectedItem);
        _inventoryController.ClearSelectedItem();
    }

    private void DeEquip(EquipmentSlot slot)
    {
        if (slot is WeaponSlotOne)
        {
            DeEquipWeaponOne();
        }
        _inventoryController.SetSelectedItem(slot);
        slot.ClearSlot();
    }

    private void DeEquipWeaponOne()
    {
        WeaponOne = null;
    }

    public float GetWeaponDamage()
    {
        return WeaponOne == null ? 0.0f : WeaponOne.BaseDamage;
    }
}
