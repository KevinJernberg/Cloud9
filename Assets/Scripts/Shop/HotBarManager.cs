using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// Enables the buttons for the shop based on the inventoryslots - William
/// </summary>
public class HotBarManager : MonoBehaviour
{
    public List<Button> buttons;

    public static UnityAction<int> UpdateShopSlots;

    private void OnEnable()
    {
        UpdateShopSlots += UpdateInventorySlots;
    }

    private void OnDisable()
    {
        UpdateShopSlots -= UpdateInventorySlots;
    }

    public void UpdateInventorySlots(int slots)
    {
        for (int i = 0; i < slots; i++)
        {
            Debug.Log("a");
            buttons[i].gameObject.SetActive(true);
            if (buttons[i].interactable != true)
            {
                buttons[i].interactable = true;
            }
        }
        for (int i = slots; i < buttons.Count; i++)
        {
            Debug.Log("b");
            buttons[i].gameObject.SetActive(false);
        }
        
    }
}
