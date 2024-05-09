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
    
    [SerializeField] private ItemData[] storage = new ItemData[10];
    

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
        storageCapacitySlider.value = (float)(storage.Length-spaceLeft) / storage.Length;

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
                storage[storage.Length - spaceLeft] = Inventory.itemSlots[i].item;
                Debug.Log($"{CheckStorageAmountLeft()}  {storage.Length}");
                amountAdded++;
                storageCapacitySlider.value = (float)(storage.Length-spaceLeft+1) / storage.Length;
            }
        }
        yield return new WaitForSeconds(waitTimeAfter);
        
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