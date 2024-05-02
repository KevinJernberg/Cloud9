using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HotBarInventory : MonoBehaviour
{

    public static UnityAction updateInventoryCount;

    private static List<HotBarSlot> hotBarSlots = new List<HotBarSlot>();


    [SerializeField] private ItemData testItem;

    private void OnEnable()
    {
        updateInventoryCount += SetSlots;
    }
    
    private void OnDisable()
    {
        updateInventoryCount -= SetSlots;
    }

    private void Start()
    {
        HotBarSlot[] slots = transform.GetComponentsInChildren<HotBarSlot>();
        foreach (HotBarSlot slot in slots)
        {
            hotBarSlots.Add(slot);
        }

        Inventory.ChangeItemAmount(4, testItem);
        
    }

    private void SetSlots()
    {
        Debug.Log("yes1.5");
        //TODO: Check if there are 2 many items for slots
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            Debug.Log("yes1.6");
            if (Inventory.itemSlots[i].item == null)
                continue;
            Debug.Log("yes1.7");
            hotBarSlots[i].SetItem(Inventory.itemSlots[i]);
        }
    }
}
