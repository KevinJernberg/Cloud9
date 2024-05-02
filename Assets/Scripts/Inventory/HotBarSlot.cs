using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotBarSlot : MonoBehaviour
{
    public ItemSlot currentItem;
    public int itemAmount;
    
    [SerializeField] private GameObject inventoryFullIndicator;
    
    [SerializeField] private TextMeshProUGUI inventoryCountUI; 


    public void SetItem(ItemSlot item)
    {
        currentItem = item;
        itemAmount = item.itemCount;
        inventoryCountUI.text = $"{item.itemCount}";
    }

    private void ChangeItemAmount(int amountChanged)
    {
        itemAmount += amountChanged;
    }

    private void SetItemAmount(int amountChanged)
    {
        
    }
}
