using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Makes it so that every text has the same size as the smallest one - William
/// </summary>
public class TextManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<TextMeshProUGUI> prices;
    
    
    private void OnEnable()
    {
        BuyUpgrade.OnBuy += UpdatePrice;
    }

    private void OnDisable()
    {
        BuyUpgrade.OnBuy -= UpdatePrice;
    }
    
    void Start()
    {
        float size = texts[0].fontSize;
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.fontSize < size)
            {
                size = text.fontSize;
            }
        }
        foreach (TextMeshProUGUI text in texts)
        {
            text.enableAutoSizing = false;
            text.fontSize = size;
        }
    }

    private void UpdatePrice(int price, int slot)
    {
        prices[slot].text = $"{price}$";
    }
}
