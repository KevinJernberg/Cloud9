using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// Inventory for creatures - Linnéa
///
/// Script no longer in use, replaced by the HotBar inventory system - Kevin
///
/// I Yield - Kevin
/// </summary>
public static class Inventory
{
    private static int _maxInventorySpace;
    
    public static List<ItemSlot> itemSlots = new List<ItemSlot>(){new ItemSlot(),new ItemSlot(),new ItemSlot(),new ItemSlot(),new ItemSlot()};

    
    /// <summary>
    /// Adds specific amount to inventory.
    /// </summary>
    /// <param name="amount">The amount to add</param>
    /// <param name="item">The type of item to add</param>
    /// <returns>Returns true if inventory is not full, and adds toAdd to the inventory. Returns false if inventory is full
    /// and adds the item if it does not already exist.</returns>
    public static bool ChangeItemAmount(int amount, ItemData item)
    {
        if (item == null)
            return false;
        if (FindItemSlot(item, out ItemSlot foundItemSlot))
        {
            Debug.Log("Yes");
            foundItemSlot.itemCount += amount;
            foundItemSlot.item = item;
            HotBarInventory.updateInventoryCount?.Invoke();
        }

        return false;
    }

    private static bool FindItemSlot(ItemData item, out ItemSlot foundSlot)
    {
        foundSlot = null;
        if (item == null)
            return false;
        
        foreach (ItemSlot slot in itemSlots)
        {
            // Check if item is in inventory
            if (slot.item != item)
                continue;
            foundSlot = slot;
            return true;
        }
        
        foreach (ItemSlot slot in itemSlots)
        {
            // Add new item to slot if needs new slot
            if (slot.item != null)
                continue;
            foundSlot = slot;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the max amount of creatures in inventory, if the value is bigger than the current amount of creatures in inventory.
    /// If overrideInventory is set to true it sets the inventory space to the value regardless if it is smaller than
    /// the currently collected creatures amount.
    /// </summary>
    /// <param name="value">The value to set the inventory to</param>
    /// <param name="overrideInventory">If true inventory limit is set to value regardless of how many creatures collected</param>
    /// <returns>True if inventory max is changed, false otherwise</returns>
    public static bool SetMaxInventorySpace(int value, bool overrideInventory = false)
    {
        // if(overrideInventory)
        // {
        //     _maxInventorySpace = value;
        //     return true;
        // }
        // if (value > _collectedCreatures)
        // {
        //     _maxInventorySpace = value;
        //     return true;
        // }
        //
        return false;
    }
}

public class ItemSlot
{
    public int itemCount;
    public ItemData item = null;
}
