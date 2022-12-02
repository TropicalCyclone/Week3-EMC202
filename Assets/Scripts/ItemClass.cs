using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ItemClass : ScriptableObject
{
    
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    [TextArea(10, 10)]
    public string itemDesc;
    public bool isStackable = true;

    public virtual void Use(InventoryManager manager)
    {
        Debug.Log("Used Item");
    }

    public virtual ItemClass GetItem() { return this; }
    public virtual EquipmentClass GetTool() { return null; }
    public virtual MiscClass GetMisc() { return null; }
    public virtual ConsumableClass GetConsumable() { return null; }

    
}
