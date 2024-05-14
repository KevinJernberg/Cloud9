using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Holds the event for when you try to buy a backpack upgrade - William
/// </summary>
public class BuyBackpack : MonoBehaviour
{
    public static UnityAction OnBuy;
    
    public void Buy()
    {
        OnBuy?.Invoke();
    }
}