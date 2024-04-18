using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SmallSuckInventoryGUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryFullIndicator;
        

    [SerializeField] private TextMeshProUGUI inventoryCount; 
    
    public static UnityAction<int, int> updateInventoryCount;

    private void OnEnable()
    {
        updateInventoryCount += UpdateInventoryStatus;
        CreatureInventory.AddToInventory(0); //Cursed AF but starts the inventory at the right amount; TODO: Make this more normal and not cursed
    }

    private void OnDisable()
    {
        updateInventoryCount -= UpdateInventoryStatus;
    }
    
    private void UpdateInventoryStatus(int amount, int capacity)
    {
        inventoryFullIndicator.SetActive(amount >= capacity);
        inventoryCount.text = $"{amount} / {capacity}";
    }

}
