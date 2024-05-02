using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "Scriptables/Items/Item")]
[Serializable]
public class ItemData : ScriptableObject
{
    public int itemID;
    public Sprite itemSprite;
    public int stackSize;
    public int sellingPrice;

}
