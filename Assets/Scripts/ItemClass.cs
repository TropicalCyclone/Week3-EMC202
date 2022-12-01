using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class ItemClass : ScriptableObject
{
    public string ID;
    public string itemName;
    public Sprite itemIcon;
    [TextArea(10, 10)]
    public string itemDesc;
    public bool isStackable = true;

    public abstract ItemClass GetItem();
    public abstract EquipmentClass GetTool();
    public abstract MiscClass GetMisc();
    public abstract ConsumableClass GetConsumable();
}
