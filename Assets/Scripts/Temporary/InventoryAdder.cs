using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryAdder : MonoBehaviour
{
    [SerializeField] private List<ItemData> randomItems;

    public void AddItem()
    {
        Debug.Log(randomItems[Random.Range(0, randomItems.Count)].itemID);
        Inventory.ChangeItemAmount(3, randomItems[Random.Range(0, randomItems.Count)]);
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
    }
}