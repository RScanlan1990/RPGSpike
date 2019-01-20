using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{
    private InventorySlot[] _inventorySlots;
    private Item _selectedItem;
    private PlayerUiController _playerUiController;
    private CameraController _camera;

    private void Start()
    {
        _inventorySlots = gameObject.transform.Find("Canvas")
                                   .Find("Background")
                                   .Find("Inventory_Slots").GetComponentsInChildren<InventorySlot>();
        _playerUiController = GetComponent<PlayerUiController>();
        _camera = GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(HaveItemSelected() && !EventSystem.current.IsPointerOverGameObject())
            {
                var ray = new RayCastWrapper(_camera.GetCamera());
                var hit = ray.CastRayFromCamera(null);
                DropItem(hit.point);
            }
        }
    }

    public void TryPickUpItem(Loot loot)
    {
        var stored = false;
        foreach (var slot in _inventorySlots)
        {
            if (slot.HaveItem() == false)
            {
                PickUpItem(slot, loot);
                stored = true;
                break;
            }
        }
        
        if(stored == false)
        {
            _selectedItem = loot.Item;
        }
    }

    private void PickUpItem(InventorySlot slot, Loot loot)
    {
        slot.AddItem(loot.Item);
        Destroy(loot.gameObject);
    }

    public void DropItem(Vector3 clickedPosition)
    {
        var heading = clickedPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var dropedPosition = transform.position + (direction * 1.0f);
        Instantiate(_selectedItem.Graphics, new Vector3(dropedPosition.x, transform.position.y, dropedPosition.z), transform.rotation);
        _selectedItem = null;
        _playerUiController.SetMouseIcon(null);
    }

    public bool HaveItemSelected()
    {
        return _selectedItem != null;
    }
    
    public void SlotClicked(InventorySlot slot, Item previousItem)
    {
        if (_selectedItem != null)
        {
            slot.AddItem(_selectedItem);
            _selectedItem = null;
            _playerUiController.SetMouseIcon(null);
        }
        
        if(previousItem != null)
        {
            _selectedItem = previousItem;
            _playerUiController.SetMouseIcon(_selectedItem == null ? null : _selectedItem.Image);
        }
    }

    public Item GetSelectedItem()
    {
        return _selectedItem;
    }

    public void ClearSelectedItem()
    {
        _selectedItem = null;
    }

    public void SetSelectedItem(EquipmentSlot slot)
    {
        var slotItem = slot.GetItem();
        _selectedItem = slotItem;
        _playerUiController.SetMouseIcon(slotItem == null ? null : slotItem.Image);
    }
}