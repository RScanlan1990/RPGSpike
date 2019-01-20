using UnityEngine;

public class Interact : MonoBehaviour
{
    private InventoryController _inventoryController;

    private void Start()
    {
        _inventoryController = GetComponent<InventoryController>();
    }

    public void TryInteract(GameObject target)
    {
        var interactable = target.GetComponent<Interactable>();
        if(interactable == null) { return; }
        DoInteract(interactable);
    }

    private void DoInteract(Interactable interactable)
    {
        switch (interactable)
        {
            case Loot loot:
                _inventoryController.TryPickUpItem(loot);
                break;
        }
    }
}
