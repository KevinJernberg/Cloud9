using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HotBarInventory : MonoBehaviour
{

    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private GameObject inventoryFullIndicator;
        

    [SerializeField] private TextMeshProUGUI inventoryCountUI; 
    
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


    private void GetHotBarSlots()
    {
        
    }
    
    private void UpdateInventoryStatus(int amount, int id)
    {
        
    }

    private bool CheckForEmptySpace()
    {
        return false;
    }
}
