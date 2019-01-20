using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image Icon;
    protected Item _item;

    protected virtual void Start()
    {
        Button button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(SlotClicked);
    }

    public bool HaveItem()
    {
        return _item != null;
    }

    public Item GetItem()
    {
        return _item;
    }

    public virtual void AddItem(Item item)
    {
        _item = item;
        Icon.sprite = _item.Image;
        Icon.enabled = true;
    }

    public virtual void ClearSlot()
    {
        _item = null;
        Icon.enabled = false;
    }

    protected virtual void SlotClicked()
    {
        if (HaveItem())
        {
            ClearSlot();
        }
    }
}