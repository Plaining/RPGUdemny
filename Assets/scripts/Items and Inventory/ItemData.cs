using UnityEngine;

public enum ItemType
{
    Materal,
    Equipment
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string itemName;
    public Sprite icon;
}
