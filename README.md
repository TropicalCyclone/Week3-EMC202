# Week3-EMC202

# INVENTORY MANAGEMENT SYSTEM
---------------------
This is a inventory management system created in Unity.

-------------

# Item Classes:

## Item Class

It is a default class that contains the itemID, itemName, ItemIcon, and Item Description. This class is the Parent claas of all other classes.

## Misc Class

This item class contains all miscellaneous items that are not Consumable nor Equipment class.

## Utility Class

This item class Has tooltypes where .

-------------

# Navigation:

## Picking up an item

- left click on an Item = Pick up Item

- Right click on an Item = Pick up half the amount of an item

----------------
## Placing an Item

- left click on an empty slot = Place all items to slot 

- left click on slot with same item = combine items

- left click on slot with diffferent item = swap items

- Right Click on an empty slot = Place 1 item into slot

- Right click on slot with same item = add one item to slot

----------------
## Double Clicking An Item
- Double click Misc Class does nothing

- Double click Equipment Class gives a debug log of the name, Tooltype, and Description

- Double clicking the Consumable class gives a debug log of the name, and comsumes one of the items.

----------------
## Dropping an Item

- placing item on left or right side of inventory, gives a prompt.

- pressing yes drops the item.

- pressing no replaces the item on the inventory

---------------------------

## Debug Menu

- can add items using the item ID of the item


