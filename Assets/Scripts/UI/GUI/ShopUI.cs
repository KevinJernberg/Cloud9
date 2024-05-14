using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> shopObjects;
    
    public void ToggleShopUI(bool active)
    {
        for (int i = 0; i < shopObjects.Count; i++)
        {
            shopObjects[i].SetActive(active);
        }
    }
}
