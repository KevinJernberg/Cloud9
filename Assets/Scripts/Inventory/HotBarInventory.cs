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
    [SerializeField] private ItemData testItem1;

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

        ResetSlots();
        Inventory.ChangeItemAmount(5, testItem);
        Inventory.ChangeItemAmount(2, testItem1);

    }

    private void SetSlots()
    {
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            if (Inventory.itemSlots[i].item == null)
                continue;
            hotBarSlots[i].SetItem(Inventory.itemSlots[i]);
        }
    }

    private void ResetSlots()
    {
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            hotBarSlots[i].RemoveItem();
        }
    }
}
