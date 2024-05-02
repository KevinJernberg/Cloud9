using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HotBarInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryFullIndicator;
    
    [SerializeField] private TextMeshProUGUI inventoryCountUI; 
    
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
        
        Inventory.AddToInventory(10, testItem);
    }

    private void SetSlots()
    {
        //TODO: Check if there are 2 many items for slots
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            hotBarSlots[i].SetItem(Inventory.itemSlots[i]);
        }
    }
}
