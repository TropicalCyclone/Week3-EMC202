using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public float healthAdded;
    public override void Use(InventoryManager manager)
    {
        Debug.Log("Name: " + itemName);
        Debug.Log("Consumable Eaten");
        manager.UseItem();

    }
    public override ConsumableClass GetConsumable() { return this; }

}

