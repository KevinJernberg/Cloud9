using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// Inventory for creatures - Linnéa
/// </summary>
public static class CreatureInventory
{
    private static int _collectedCreatures;
    private static int _maxInventorySpace;

    /// <summary>
    /// Adds specific amount to inventory.
    /// </summary>
    /// <param name="toAdd">The amount to add</param>
    /// <returns>Returns true if inventory is not full, and adds toAdd to the inventory. Returns false if inventory is full
    /// and does not add to inventory.</returns>
    public static bool AddToInventory(int toAdd)
    {
        if(_collectedCreatures < _maxInventorySpace)
        {
            _collectedCreatures += toAdd;
            Debug.Log($"Added to inventory. Inventory now contains: {_collectedCreatures}");
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
        if(overrideInventory)
        {
            _maxInventorySpace = value;
            return true;
        }
        if (value > _collectedCreatures)
        {
            _maxInventorySpace = value;
            return true;
        }

        return false;
    }
}
