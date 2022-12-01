using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject itemCursor;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject hotbarSlotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private SlotClass[] startingItems;

    public SlotClass[] items;

    [SerializeField] private DialogBoxUI DialogBox;

    private GameObject[] slots;
    private GameObject[] hotbarSlots;

    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;
    bool isMovingItem;

    private float lastClickTime;
    public float catchTime = 0.10f;

    [SerializeField] private GameObject hotbarSelector;
    [SerializeField] private int selectedSlotIndex = 0;
    public ItemClass selectedItem;

    private void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        hotbarSlots = new GameObject[hotbarSlotHolder.transform.childCount];
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i] = hotbarSlotHolder.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];
        }


        for (int i = 0; i < slotHolder.transform.childCount; i++) 
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

        RefreshUI();
        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }

    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
            try
            {
                itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
            }
            catch
            {
                isMovingItem = false;
            }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < catchTime)
            {
                try
                {
                    movingSlot.GetItem().Use(this);
                    lastClickTime = catchTime;
                }
                catch
                {

                }
            }
            else
            {
                if (isMovingItem)
                {
                    EndItemMove();
                }
                else
                {
                    BeginItemMove();
                }
            }
            lastClickTime = Time.time;
            
        }

        else if (Input.GetMouseButtonDown(1))
        {

                if (isMovingItem)
                {
                  EndItemMove_Single();
                }
                else
                {
                    BeginItemMove_Half();
                }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1,0,hotbarSlots.Length -1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, hotbarSlots.Length -1);
        }
            hotbarSelector.transform.position = hotbarSlots[selectedSlotIndex].transform.position;
        selectedItem = items[selectedSlotIndex + (hotbarSlots.Length * 3)].GetItem();
    }

    #region Inventory Utils

    public void RefreshUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().isStackable)
                    if (items[i].getQuantity() <= 1)
                        slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                    else
                        slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].getQuantity() + "";
                        
                else
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";

            }
        }
        RefreshHotbar();
    }
    
    public void RefreshHotbar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
            
        {
            try
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i+(hotbarSlots.Length*3)].GetItem().itemIcon;
                if (items[i + (hotbarSlots.Length * 3)].GetItem().isStackable)
                    if (items[i + (hotbarSlots.Length * 3)].getQuantity() <= 1)
                        slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                    else
                        hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = items[i + (hotbarSlots.Length * 3)].getQuantity() + "";

                else
                    hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                hotbarSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";

            }
        }
    }
    public bool Add(ItemClass item, int quantity)
    {
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        slot.AddQuantity(quantity);
        else
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
        }
        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.getQuantity() > 1)
            temp.SubQuantity(1);
            else
            {
                int DeleteSlotIndex = 0;
                for(int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        DeleteSlotIndex = i;
                        break;
                    }
                }

                items[DeleteSlotIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }

    public void UseItem()
    {
        movingSlot.SubQuantity(1);
        if(movingSlot.getQuantity() <= 0)
        {
            movingSlot.Clear();
        }
    }
    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
                return items[i];
        }
        return null;
    }
    #endregion Inventory Utils

    #region Movement
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
        {
            return false;
        }
        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }

    private bool BeginItemMove_Half()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null|| originalSlot.GetItem() == null)
        {
            return false;
        }
        
        movingSlot = new SlotClass(originalSlot.GetItem(),Mathf.CeilToInt(originalSlot.getQuantity()/2f));
        originalSlot.SubQuantity(Mathf.CeilToInt(originalSlot.getQuantity() / 2f));
        if(originalSlot.getQuantity() == 0)
        {
            originalSlot.Clear();
        }
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool EndItemMove()
    {
        ItemClass itemStore = movingSlot.GetItem();
        int Quant = movingSlot.getQuantity();
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            int Pick = DialogBox.Show();
            if (Pick == 1)
            {
                movingSlot.Clear();
            }
                else if (Pick == 2)
            {
                Add(itemStore, Quant);
                movingSlot.Clear();
            }
            
        }
        else
        {

            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.getQuantity());
                        movingSlot.Clear();
                    }
                    else
                        return false;
                }
                else
                {
                    tempSlot = new SlotClass(originalSlot);
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.getQuantity());
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.getQuantity());
                    RefreshUI();
                    return true;
                }


            }
            else
            {

                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.getQuantity());
                movingSlot.Clear();

            }
        }

        isMovingItem=false;
        RefreshUI();
        return true;
    }

    private bool EndItemMove_Single()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            return false;
        }
        else if (originalSlot.GetItem() != null && originalSlot.GetItem() != movingSlot.GetItem())
            return false;
  
        movingSlot.SubQuantity(1);
        if(originalSlot.GetItem() != null && originalSlot.GetItem() == movingSlot.GetItem())
        {
            originalSlot.AddQuantity(1);
        }
        else
            originalSlot.AddItem(movingSlot.GetItem(), 1);

        if (movingSlot.getQuantity() < 1)
        {
            isMovingItem = false;
            movingSlot.Clear();
        }
        else
        {
            isMovingItem = true;
        }
        
        RefreshUI();
        return true;
    }
    private SlotClass GetClosestSlot()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 33)
            {
                return items[i];
            }
        }
        return null;
    }
    #endregion

    public SlotClass ReturnClass()
    {
        return movingSlot;
    }
}
