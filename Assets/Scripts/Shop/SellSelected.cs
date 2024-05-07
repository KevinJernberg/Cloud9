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
                Debug.Log($"Selling for {Inventory.itemSlots[selected].item.sellingPrice* Inventory.itemSlots[selected].itemCount}");
                Inventory.ChangeItemAmount(-Inventory.itemSlots[selected].itemCount, Inventory.itemSlots[selected].item);
                
                Inventory.ChangeCoinAmount(Inventory.itemSlots[selected].item.sellingPrice * Inventory.itemSlots[selected].itemCount);
            }
        }
        
        
    }
    public void SellAll()
    {
        //Todo: Make it so that the items gets removed from the inventory and adds the price to a money counter
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            if (Inventory.itemSlots[i].item != null)
            {
                Debug.Log($"Selling for {Inventory.itemSlots[i].item.sellingPrice* Inventory.itemSlots[i].itemCount}");
                Inventory.ChangeItemAmount(-Inventory.itemSlots[i].itemCount, Inventory.itemSlots[i].item);
                
            }
        }
    }
}
