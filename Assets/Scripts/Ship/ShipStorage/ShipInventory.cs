using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipInventory
{
    public static ItemData[] storage = new ItemData[10];

    public static void ExpandInventory(int sizeIncreaseAmount)
    {
        ItemData[] newStorageAmount = new ItemData[storage.Length + sizeIncreaseAmount];

        for (int i = 0; i < storage.Length; i++)
        {
            newStorageAmount[i] = storage[i];
        }

        storage = newStorageAmount;
    }
}
