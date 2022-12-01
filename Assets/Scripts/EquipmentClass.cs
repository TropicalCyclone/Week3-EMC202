using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment Class", menuName = "Item/Equipment")]
public class EquipmentClass : ItemClass
{
    [Header("Equipment")]
    public ToolType toolType;

    public enum ToolType
    {
        weapon,
        pickaxe,
        hammer,
        axe
    }
    public override ItemClass GetItem() { return this; }
    public override EquipmentClass GetTool() { return this; }
    public override MiscClass GetMisc() { return null; }
    public override ConsumableClass GetConsumable() { return null; }

}
