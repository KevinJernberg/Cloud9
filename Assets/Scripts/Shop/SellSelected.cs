using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// System for selecting a slot in the hotbar and then selling it - William
/// </summary>
public class SellSelected : MonoBehaviour
{
    [SerializeField] private EventSystem system;
    private int selected = 5;
    [SerializeField] private ShopingList list;

    public void Select(int slot)
    {
        selected = slot;
    }
    public void Sell()
    {
        //Todo: Make it so that the items gets removed from the inventory and adds the price to a money counter
        if (selected is < -1 or < 5)
        {
            if (Inventory.itemSlots[selected].item != null)
            {
                if (Contain(list.item, Inventory.itemSlots[selected].item))
                {
                    Debug.Log($"Selling for {list.sellPrice[Find(list.item,Inventory.itemSlots[selected].item)] * Inventory.itemSlots[selected].itemCount}");
                    Inventory.ChangeCoinAmount(list.sellPrice[Find(list.item,Inventory.itemSlots[selected].item)] * Inventory.itemSlots[selected].itemCount);
                    Inventory.ChangeItemAmount(-Inventory.itemSlots[selected].itemCount, Inventory.itemSlots[selected].item);
                    
                }
                else
                {
                    Debug.Log($"This shop is not buying: {Inventory.itemSlots[selected].item.name}");
                }
                
            }
        }
        
        
    }
    public void SellAll()
    {
        //Todo: Make it so that the items gets removed from the inventory and adds the price to a money counter
        int sellammount = 0;
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            if (Inventory.itemSlots[i].item != null)
            {
                if (Contain(list.item, Inventory.itemSlots[i].item))
                {
                    sellammount+=(list.sellPrice[Find(list.item,Inventory.itemSlots[i].item)] * Inventory.itemSlots[i].itemCount);
                    Inventory.ChangeItemAmount(-Inventory.itemSlots[i].itemCount, Inventory.itemSlots[i].item);
                }
                else
                {
                    Debug.Log($"This shop is not buying: {Inventory.itemSlots[i].item.name}");
                }
            }
        }
        Debug.Log($"Selling  all items for {sellammount}");
        Inventory.ChangeCoinAmount(sellammount);
    }

    private bool Contain(ItemData[] original, ItemData item)
    {
        for (int i = 0; i < original.Length; i++)
        {
            if (original[i] == item)
            {
                return true;
            }
        }

        return false;
    }
    
    private int Find(ItemData[] original, ItemData item)
    {
        for (int i = 0; i < original.Length; i++)
        {
            if (original[i] == item)
            {
                return i;
            }
        }

        return -1;
    }
}
