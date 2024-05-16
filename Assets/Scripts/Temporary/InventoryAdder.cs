using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryAdder : MonoBehaviour
{
    [SerializeField] private List<ItemData> randomItems;

    public void AddItem()
    {
        Inventory.ChangeItemAmount(3, randomItems[Random.Range(0, randomItems.Count)]);
    }
    public void ChangeCoins(int amount)
    {
        Inventory.ChangeCoinAmount(amount);
    }
}

[CustomEditor(typeof(InventoryAdder))]
public class InventoryAdderEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        var t = (target as InventoryAdder);
        base.OnInspectorGUI();
        if (GUILayout.Button("Add item"))
        {
            t.AddItem();
        }
        if (GUILayout.Button("Coin up"))
        {
            t.ChangeCoins(500);
        }
        if (GUILayout.Button("BrokeBoy"))
        {
            t.ChangeCoins(-20);
        }
    }
}