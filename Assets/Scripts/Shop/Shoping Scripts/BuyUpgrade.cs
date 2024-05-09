using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages all the upgrade choices for the shop - William
/// </summary>
public class BuyUpgrade : MonoBehaviour
{
    public int suckUpgradeLimit;
    public int travelUpgradeLimit;
    public int backPackUpgradeLimit;
    public int shipSpaceUpgradeLimit;
    private int suckUpgradeAmmount;
    private int travelUpgradeAmmount;
    private int backPackUpgradeAmmount;
    private int shipSpaceUpgradeAmmount;
    public void BuySuck()
    {
        //TODO: BetterSuck
        Debug.Log("bought suck");
    }
    public void BuyTravel()
    {
        //TODO: Travel further
        Debug.Log("bought travel");
    }
    public void BuyBackPack()
    {
        Debug.Log("bought backpack");
        Inventory.AddInventorySlots(1);
    }
    public void BuyShipSpace()
    {
        //TODO: Larger ShipStorage
        Debug.Log("bought ShipStorage");
    }
}
