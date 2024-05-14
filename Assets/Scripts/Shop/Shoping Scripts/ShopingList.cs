using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/Shop/ShopingList")]
/// <summary>
/// Holds the data about what each item should be sold for in a shop - William
/// </summary>
public class ShopingList : ScriptableObject
{
    public ItemData[] item;
    public int[] sellPrice;
}
