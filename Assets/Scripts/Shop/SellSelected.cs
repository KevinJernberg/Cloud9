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

    private int selected;


    public void Select(int slot)
    {
        selected = slot;
    }
    public void Sell()
    {
        Debug.Log($"Selling for {Inventory.itemSlots[selected].item.sellingPrice}");
        //Debug.Log($"Sell {selected.name}");
    }
}
