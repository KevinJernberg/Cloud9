using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages all the upgrade choices for the shop - William
/// </summary>
public class BuyUpgrade : MonoBehaviour
{
    public static UnityAction<int, int> OnBuy;

    public List<int> SuckPrices;
    public List<int> TravelPrices;
    public List<int> BackPackPrices;
    public List<int> ShipSpacePrices;

    private  int suckUpgradeAmmount;
    private  int travelUpgradeAmmount;
    private  int backPackUpgradeAmmount;
    private  int shipSpaceUpgradeAmmount;
    

    public void BuySuck()
    {
        //TODO: BetterSuck
        
        if (suckUpgradeAmmount < SuckPrices.Count)
        {
            if (Inventory.ChangeCoinAmount(-SuckPrices[suckUpgradeAmmount]))
            {
                Debug.Log("bought suck");
                suckUpgradeAmmount++;
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            Debug.Log("no suck upgrades available");
        }
    }
    public void BuyTravel()
    {
        //TODO: Travel further
        
        if (travelUpgradeAmmount < TravelPrices.Count)
        {
            if (Inventory.ChangeCoinAmount(-TravelPrices[travelUpgradeAmmount]))
            {
                Debug.Log("bought travel");
                travelUpgradeAmmount++;
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            Debug.Log("no travel upgrades available");
        }
    }
    public void BuyBackPack()
    {
        if (backPackUpgradeAmmount < BackPackPrices.Count)
        {
            if (Inventory.ChangeCoinAmount(-BackPackPrices[backPackUpgradeAmmount]))
            {
                Debug.Log("bought backpack");
                Inventory.AddInventorySlots(1);
                backPackUpgradeAmmount++;
                OnBuy?.Invoke(BackPackPrices[backPackUpgradeAmmount],2);
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            Debug.Log("no backpack upgrades available");
        }
        
    }
    public void BuyShipSpace()
    {
        //TODO: Larger ShipStorage
        
        if (shipSpaceUpgradeAmmount < ShipSpacePrices.Count)
        {
            if (Inventory.ChangeCoinAmount(-ShipSpacePrices[shipSpaceUpgradeAmmount]))
            {
                Debug.Log("bought ShipStorage");
                shipSpaceUpgradeAmmount++;
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            Debug.Log("no ShipStorage upgrades available");
        }
    }
}
