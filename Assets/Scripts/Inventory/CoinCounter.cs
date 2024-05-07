using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinCounter : MonoBehaviour
{
    public static UnityAction<int> updateCoinCount;

    [SerializeField] private TextMeshProUGUI coinText; 
    private void OnEnable()
    {
        updateCoinCount += UpdateCoinAmount;
    }
    
    private void OnDisable()
    {
        updateCoinCount -= UpdateCoinAmount;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"> The amount of coins that should be displayed on the GUI</param>
    private void UpdateCoinAmount(int amount)
    {
        if (amount > 9999)
        {
            coinText.text = "+9999";
            return;
        }
        coinText.text = amount.ToString();
    }
}
