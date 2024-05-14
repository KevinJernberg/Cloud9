using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BuyTravel : MonoBehaviour
{
    public static UnityAction OnBuy;
    
    public void Buy()
    {
        OnBuy?.Invoke();
    }
}
