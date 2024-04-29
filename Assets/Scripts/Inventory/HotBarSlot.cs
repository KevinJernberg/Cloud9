using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSlot : MonoBehaviour
{
    public ItemData currentItem;
    public int itemAmount;


    public void SetItem(ItemData item)
    {
        currentItem = item;
        itemAmount = default;
    }

    private void ChangeItemAmount(int amountChanged)
    {
        itemAmount += amountChanged;
    }

    private void SetItemAmount(int amountChanged)
    {
        
    }
}
