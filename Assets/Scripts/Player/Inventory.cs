using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    private static int _maxInventorySpace = 5;

    private static int _coinAmount = 0;
    
    public static List<ItemSlot> itemSlots = new List<ItemSlot>(){new ItemSlot(),new ItemSlot(),new ItemSlot()};

    public static UnityAction<int> slotAmountUpdated;


    /// <summary>
    /// Adds specific amount to inventory.
    /// </summary>
    /// <param name="amount">The amount to add</param>
    /// <param name="item">The type of item to add</param>
    /// <returns>Returns true if inventory is not full, and adds toAdd to the inventory. Returns false if inventory is full
    /// and adds the item if it does not already exist.</returns>
    public static bool ChangeItemAmount(int amount, ItemData item)
    {
        if (item == null || amount == 0)
            return false;
        if (FindItemSlot(item, out ItemSlot foundItemSlot))
        {
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
    /// 
    /// </summary>
    /// <returns>False if there are no more slot spaces, true if size is changed</returns>
    public static bool AddInventorySlots(int slotAmountToAdd)
    {
        if (itemSlots.Count < _maxInventorySpace)
        {
            int slotAmountLeft = slotAmountToAdd > _maxInventorySpace-itemSlots.Count ? _maxInventorySpace-itemSlots.Count : slotAmountToAdd;
            for (int i = 0; i < slotAmountLeft; i++)
            {
                itemSlots.Add(new ItemSlot());
            }
            slotAmountUpdated?.Invoke(itemSlots.Count);
            return true;
        }
        return false;
    }

    
    public static bool SetMaxInventorySpace(int amount){ return false; }

    /// <summary>
    /// Changes the amount of coins the player holds
    /// </summary>
    /// <param name="amount">The amount of coins, negative amount indicate subtraction</param>
    /// <returns>False if amount removed is more than the current coin amount, otherwise true</returns>
    public static bool ChangeCoinAmount(int amount)
    {
        if (-amount > _coinAmount)
            return false;
        _coinAmount += amount;
        CoinCounter.updateCoinCount?.Invoke(_coinAmount);
        return true;
    }

}

public class ItemSlot
{
    public int itemCount;
    public ItemData item = null;
}
