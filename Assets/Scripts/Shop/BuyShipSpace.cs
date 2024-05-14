using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Holds the event for when you try to buy a shipspace upgrade - William
/// </summary>
public class BuyShipSpace : MonoBehaviour
{
    public static UnityAction OnBuy;
    
    public void Buy()
    {
        OnBuy?.Invoke();
    }
}
