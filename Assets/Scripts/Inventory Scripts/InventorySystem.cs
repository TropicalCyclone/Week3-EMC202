using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public UnityAction<InventorySlot> OnInventorySlotChanged;
    public int InventorySize => InventorySlots.Count;

    [SerializeField]
    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd,int amountToAdd)
    {
        InventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd);
        return true;
    }
}
