﻿using System;

namespace AdventureGame.Engine
{

    public class InventoryComponent : Component
    {
        //public string InventoryId { get; private set; }
        public int InventorySize { get; private set; }
        public Item[] InventoryItems { get; private set; }
        public Tags Tags { get; set; }

        public InventoryComponent(int inventorySize = 20, string type = "chest")
        {
            InventorySize = inventorySize;
            InventoryItems = new Item[inventorySize];
            Tags = new Tags(type);
        }

        // Add an item to the inventory
        // Return the inserted position or -1 if there is not enough space
        public Item AddItem(Item item)
        {
            //int insertedPosition = -1;
            //int quantity = item.Quantity;

            // Check if the item doesn't hold the maximum quantity already
            if (item.Quantity < item.StackSize)
            {
                // Check if the item can be stacked with an existing inventory item
                for (int i = 0; i < InventoryItems.Length; i++)
                {
                    Item currentItem = InventoryItems[i];

                    if (currentItem != null)
                    {
                        if (currentItem.ItemId == item.ItemId
                            && currentItem.Quantity < currentItem.StackSize)
                        {
                            if (currentItem.Quantity + item.Quantity <= currentItem.StackSize)
                            {
                                // The quantity can be added to the current item
                                InventoryItems[i].IncreaseQuantity(item.Quantity);
                                //insertedPosition = i;
                                //break;

                                // DELETE the Item?? Does it exist anywhere in memory?
                                // Does this effectively delete Item??
                                item = null;
                                return item;
                            }
                            else
                            {
                                // Add as much as possible to the current item's quantity
                                int availableSpace = currentItem.StackSize - currentItem.Quantity;
                                InventoryItems[i].IncreaseQuantity(availableSpace);
                                item.Quantity -= availableSpace;

                                //int remainingQuantity = quantity;
                            }
                        }
                    }
                }
            }

            // Check if there is any quantity of the item remaining
            // Check if the item has already been added to existing inventory items
            if (item.Quantity > 0)
            {
                // Add the item to the next free inventory slot if there's space
                int nextSlot = FindNextFreeSlot();
                if (nextSlot != -1)
                {
                    InventoryItems[nextSlot] = item;
                    //insertedPosition = nextSlot;
                }
            }
            else
            {
                // DELETE item??
                item = null;
                return item;
            }

            //return insertedPosition;
            return item;
        }

        // Add an item to the inventory at a specified position
        // Return the item if one already exists in that position
        public Item AddItemAtPosition(Item item, int position)
        {
            Item currentItem = InventoryItems[position];

            // Check if the position is empty
            if (currentItem == null)
                InventoryItems[position] = item;
            else
            {
                // Check if the items are the same type and can stack
                if (item.ItemId == currentItem.ItemId)
                {
                    item = StackItems(item, position);
                    if (item == null || item.Quantity == 0)
                        return null;
                }
                else //if (item.Quantity > 0)
                {
                    //Item temp = currentItem;
                    InventoryItems[position] = item;
                }
            }
            return currentItem;
        }

        //
        public Item StackItems(Item item, int position)
        {
            Item existingItem = InventoryItems[position];

            if (existingItem == null)
                return null;

            // Check if there is space to stack more
            if (existingItem.ItemId == item.ItemId
                && existingItem.Quantity < existingItem.StackSize)
            {
                if (existingItem.Quantity + item.Quantity <= existingItem.StackSize)
                {
                    // The quantity can be added to the current item
                    InventoryItems[position].IncreaseQuantity(item.Quantity);
                    return null;
                }
                else
                {
                    // Add as much as possible to the current item's quantity
                    int availableSpace = existingItem.StackSize - existingItem.Quantity;
                    InventoryItems[position].IncreaseQuantity(availableSpace);
                    item.Quantity -= availableSpace;
                    // quantityDifference
                    //int remainingQuantity = quantity;
                }
            }
            return item;
        }

        // Find the next available position in the inventory list 
        public int FindNextFreeSlot()
        {
            int availablePosition = -1;

            for (int i = 0; i < InventoryItems.Length; i++)
            {
                if (InventoryItems[i] == null)
                {
                    availablePosition = i;
                    break;
                }
            }

            return availablePosition;
        }
    }

}
