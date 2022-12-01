using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment Class", menuName = "Item/Equipment")]
public class EquipmentClass : ItemClass
{
    [Header("Equipment")]
    public ToolType toolType;

    public override void Use(InventoryManager manager)
    {
        Debug.Log("Name: "+ itemName);
        Debug.Log("ToolType: "+toolType);
        Debug.Log("Description: ");
        Debug.Log(itemDesc);
    }
    public enum ToolType
    {
        weapon,
        pickaxe,
        hammer,
        axe
    }
    public override EquipmentClass GetTool() { return this; }


}
