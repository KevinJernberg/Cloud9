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
    
    public static UnityAction<int, int> updateInventoryCount;

    private static List<HotBarSlot> hotBarSlots;


    private void OnEnable()
    {
        updateInventoryCount += SetSlot;
    }
    
    private void OnDisable()
    {
        updateInventoryCount -= SetSlot;
    }

    private void Start()
    {
        HotBarSlot[] slots = transform.GetComponentsInChildren<HotBarSlot>();
        foreach (HotBarSlot slot in slots)
        {
            hotBarSlots.Add(slot);
        }
    }

    private void SetSlot(int amount, int slot)
    {
        
    }
}
