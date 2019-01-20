using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        None,
        Tool,
        Weapon,
        Armour
    }

    public ItemType Type;
    public bool IsEquipable;
    public int Id;
    public string Name;
    public Sprite Image;
    public GameObject Graphics;
    public Vector3 HandOffset;
    public Vector3 HandRotationOffset;
}
