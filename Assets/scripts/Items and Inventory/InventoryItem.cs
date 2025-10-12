using System;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public ItemData data;
    public int stackSize=1;
    public InventoryItem(ItemData _itemData)
    {
        data = _itemData;
    }
    public void AddStack()=> stackSize++;
    public void RemoveStack()=> stackSize--;
}
