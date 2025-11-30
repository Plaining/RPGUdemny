using System.Collections.Generic;
using UnityEngine;

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
        List<InventoryItem> currentStash = inventory.GetStashList();

        for (int i = currentEquipment.Count-1; i >= 0; i--)
        {
            if(Random.Range(0,100) <= chanceToLooseItems)
            {
                DropItem(currentEquipment[i].data);
                inventory.UnequipItem(currentEquipment[i].data as ItemData_Equipment);
            }
        }
        for (int i = currentStash.Count-1; i >= 0; i--)
        {
            if(Random.Range(0,100) <= chanceToLooseMaterials)
            {
                DropItem(currentStash[i].data);
            }
        }
    }
}
