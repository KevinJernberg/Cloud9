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
        BuyBackpack.OnBuy += SetSlots;
        updateInventoryCount += SetSlots;
    }
    
    private void OnDisable()
    {
        BuyBackpack.OnBuy -= SetSlots;
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
        
        //TODO: Remove Temp add items on these lower lines
        Inventory.ChangeItemAmount(3, testItem);
        Inventory.ChangeItemAmount(10, testItem1);
    }

    private void SetSlots()
    {
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            hotBarSlots[i].gameObject.SetActive(true);
            hotBarSlots[i].RemoveItem();
            if (Inventory.itemSlots[i].item == null)
                continue;
            hotBarSlots[i].SetItem(Inventory.itemSlots[i]);
        }
        for (int i = Inventory.itemSlots.Count; i < hotBarSlots.Count; i++)
        {
            hotBarSlots[i].gameObject.SetActive(false);
        }
        
        HotBarManager.UpdateShopSlots?.Invoke(Inventory.itemSlots.Count);
    }

    private void ResetSlots()
    {
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            hotBarSlots[i].RemoveItem();
        }
    }
}
