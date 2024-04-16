using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreatureInventory
{
    private static int _collectedCreatures;

    public static void AddToInventory(int toAdd)
    {
        _collectedCreatures += toAdd;
        Debug.Log($"Added to inventory. Inventory now contains: {_collectedCreatures}");
    }
}
