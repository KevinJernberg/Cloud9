using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarManager : MonoBehaviour
{
    public List<Button> buttons;

    private void OnEnable()
    {
        Inventory.slotAmountUpdated += UpdateInventorySlots;
    }

    private void OnDisable()
    {
        Inventory.slotAmountUpdated -= UpdateInventorySlots;
    }

    public void UpdateInventorySlots(int slots)
    {
        Debug.Log(slots);
        for (int i = 0; i < slots-1; i++)
        {
            if (buttons[i].interactable != true)
            {
                buttons[i].interactable = true;
            }
        }
    }
}
