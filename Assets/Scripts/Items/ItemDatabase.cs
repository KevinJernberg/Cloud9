using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Items/Database")]
public class ItemDatabase : ScriptableObject
{
    public int itemListAmount;
    public List<ItemData> itemList;

    private void OnValidate()
    {
        itemListAmount = itemList.Count;

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].itemID = i;
        }
    }
}
