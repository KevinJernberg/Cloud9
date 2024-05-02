using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlot : MonoBehaviour
{
    public ItemSlot currentItem;

    [SerializeField] private Image itemImage; 
    
    [SerializeField] private TextMeshProUGUI itemCountText; 


    public void SetItem(ItemSlot item)
    {
        currentItem = item;
        itemImage.sprite = item.item.itemSprite;
        itemCountText.text = $"{item.itemCount}";
    }

    private void ChangeItemAmount(int amountChanged)
    {
        itemCountText.text += amountChanged;
    }

    private void SetItemAmount(int amountChanged)
    {
        
    }
}
