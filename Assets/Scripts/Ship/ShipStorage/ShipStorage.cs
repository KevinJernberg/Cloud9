using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipStorage : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerInput _playerInputComponent;

    [SerializeField] private Slider storageCapacitySlider;


    [Header("Timings")]
    [SerializeField, Tooltip("The time it takes to start depositing, in seconds")] private float waitTimeUntilStart;
    [SerializeField, Tooltip("The time it take to deposit 1 item, in second")] private float waitTimeBetweenDeposits;
    [SerializeField, Tooltip("The time it takes until the deposit is done")] private float waitTimeAfter;

    [Header("Audio")] 
    public UIAudio uIAudio;

    private bool isDepositing;

    public void Interact()
    {
        if (isDepositing)
            return;
        StartCoroutine(WaitForAnimationToFinish());
    }

    

    private IEnumerator WaitForAnimationToFinish()
    {
        Debug.Log("Depositing");
        _playerInputComponent.SwitchCurrentActionMap("UI");
        isDepositing = true;

        int spaceLeft = CheckStorageAmountLeft();
        float amountToAdd = 0;
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            amountToAdd += Inventory.itemSlots[i].itemCount;
            if (amountToAdd > spaceLeft)
            {
                amountToAdd = spaceLeft;
                break;
            }
        }

        yield return new WaitForSeconds(waitTimeUntilStart);
        storageCapacitySlider.value = (float)(ShipInventory.storage.Length-spaceLeft) / ShipInventory.storage.Length;

        int amountAdded = 0;
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            for (int j = Inventory.itemSlots[i].itemCount; j > 0; j--)
            {
                spaceLeft = CheckStorageAmountLeft();
                Debug.Log(Inventory.itemSlots[i].itemCount);
                if (amountAdded >= amountToAdd)
                    break;
                if (spaceLeft <= 0)
                    break;

                yield return new WaitForSeconds(waitTimeBetweenDeposits);
                Inventory.ChangeItemAmount(-1, (Inventory.itemSlots[i].item));
                ShipInventory.storage[ShipInventory.storage.Length - spaceLeft] = Inventory.itemSlots[i].item;
                Debug.Log($"{CheckStorageAmountLeft()}  {ShipInventory.storage.Length}");
                amountAdded++;
                storageCapacitySlider.value = (float)(ShipInventory.storage.Length-spaceLeft+1) / ShipInventory.storage.Length;
            }
        }
        yield return new WaitForSeconds(waitTimeAfter);
        
        _playerInputComponent.SwitchCurrentActionMap("Player");
        Debug.Log("Deposit Complete");
    }


    private bool CheckIfFull()
    {
        int spaceLeft = CheckStorageAmountLeft();
        Debug.Log($"SPACE LEFT: {spaceLeft} \n List: {ShipInventory.storage[3]}");
        if (spaceLeft == 0)
            return true;
        return false;
    }

    private int CheckStorageAmountLeft()
    {
        int amount = 0;
        for (int i = 0; i < ShipInventory.storage.Length; i++)
        {
            if (ShipInventory.storage[i] == null)
            {
                amount = ShipInventory.storage.Length - (i);
                break;
            }
        }

        return amount;
    }
}