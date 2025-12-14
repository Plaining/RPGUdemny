using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLooseItems;
    [SerializeField] private float chanceToLooseMaterials;

    public override void GenerateDrop()
    {
        //base.GenerateDrop();
        Inventory inventory = Inventory.instance;
        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> equipmentToLose = new List<InventoryItem>();
        List<InventoryItem> currentStash = inventory.GetStashList();
        List<InventoryItem> stashToLose = new List<InventoryItem>();

        for (int i = currentEquipment.Count-1; i >= 0; i--)
        {
            if(Random.Range(0,100) <= chanceToLooseItems)
            {
                InventoryItem item = currentEquipment[i];
                DropItem(item.data);
                inventory.UnequipItem(item.data as ItemData_Equipment);
                equipmentToLose.Add(item);
            }
        }
        for (int i = currentStash.Count-1; i >= 0; i--)
        {
            if(Random.Range(0,100) <= chanceToLooseMaterials)
            {
                InventoryItem item = currentStash[i];
                DropItem(item.data);
                stashToLose.Add(item);
            }
        }

        foreach (var item in equipmentToLose)
        {
            inventory.RemoveItem(item.data);
        }
        foreach (var item in stashToLose) { inventory.RemoveItem(item.data); }
    }
}
