using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipStorage : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerInput _playerInputComponent;

    [SerializeField] private ItemData[] storage = new ItemData[10];

    [SerializeField, Tooltip("Should be the same length as the animation to deposit materials")] private float waitTimeUntilDone;

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

        int amountAdded = 0;
        for (int i = 0; i < Inventory.itemSlots.Count; i++)
        {
            for (int j = Inventory.itemSlots[i].itemCount; j > 0; j--)
            {
                spaceLeft = CheckStorageAmountLeft();
                Debug.Log(Inventory.itemSlots[i].itemCount);
                if (spaceLeft <= 0)
                    break;

                yield return new WaitForSeconds(waitTimeUntilDone / amountToAdd);
                Inventory.ChangeItemAmount(-1, (Inventory.itemSlots[i].item));
                storage[storage.Length - spaceLeft] = Inventory.itemSlots[i].item;
                amountAdded++;
                if (amountAdded >= amountToAdd)
                {
                    break;
                }
            }
        }
        
        _playerInputComponent.SwitchCurrentActionMap("Player");
        Debug.Log("Deposit Complete");
    }


    private bool CheckIfFull()
    {
        int spaceLeft = CheckStorageAmountLeft();
        Debug.Log($"SPACE LEFT: {spaceLeft} \n List: {storage[3]}");
        if (spaceLeft == 0)
            return true;
        return false;
    }

    private int CheckStorageAmountLeft()
    {
        int amount = 0;
        for (int i = 0; i < storage.Length; i++)
        {
            if (storage[i] == null)
            {
                amount = storage.Length - (i);
                break;
            }
        }

        return amount;
    }
}