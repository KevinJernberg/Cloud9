using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ShopingList))]

/// <summary>
/// Changes the inspector for the scriptableobjects made from "ShopingList" - William
/// </summary>

public class ShopingListEditor : Editor
{
    
    static readonly int indexWidth = 20;
    static readonly int itemWidth = 200;
    static readonly int sellpriceWidth = 50;

    public override void OnInspectorGUI()
    {
        //Setup
        ShopingList sl = (ShopingList)target;

        EditorGUILayout.BeginHorizontal();

        //Check for behaviours
        if (sl.item == null || sl.item.Length == 0)
        {
            EditorGUILayout.HelpBox("No Items in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
        }
        else
        {
            EditorGUILayout.BeginVertical();
               
            // Header
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(indexWidth));
            EditorGUILayout.LabelField("Items", GUILayout.Width(itemWidth));
            EditorGUILayout.LabelField("Prices", GUILayout.Width(sellpriceWidth));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < sl.item.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
 
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(indexWidth));
                sl.item[i] = (ItemData)EditorGUILayout.ObjectField(sl.item[i], typeof(ItemData), false, GUILayout.Width(itemWidth));
                sl.sellPrice[i] = EditorGUILayout.IntField(sl.sellPrice[i], GUILayout.Width(sellpriceWidth));
                   
                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(sl);
            }
            EditorGUILayout.EndVertical();
        }
        
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add Item"))
        {
            AddBehaviour(sl);
            EditorUtility.SetDirty(sl);
        }
        
        if (sl.item != null && sl.item.Length > 0)
        {
            if (GUILayout.Button("Remove Item"))
            {
                RemoveBehaviour(sl);
                EditorUtility.SetDirty(sl);
            }
        }
    }

    private void AddBehaviour(ShopingList sl)
    {
        int oldCount = (sl.item != null) ? sl.item.Length : 0;
        ItemData[] newItems = new ItemData[oldCount + 1];
        int[] newPrices = new int[oldCount + 1];
        for (int i = 0; i < oldCount; i++)
        {
            newItems[i] = sl.item[i];
            newPrices[i] = sl.sellPrice[i];
        }

        newPrices[oldCount] = 1;
        sl.item = newItems;
        sl.sellPrice = newPrices;
    }
    private void RemoveBehaviour(ShopingList sl)
    {
        int oldCount = sl.item.Length;
        if (oldCount == 1)
        {
            sl.item = null;
            sl.sellPrice = null;
            return;
        }
        ItemData[] newItems = new ItemData[oldCount - 1];
        int[] newPrices = new int[oldCount - 1];
        for (int i = 0; i < oldCount-1; i++)
        {
            newItems[i] = sl.item[i];
            newPrices[i] = sl.sellPrice[i];
        }
        sl.item = newItems;
        sl.sellPrice = newPrices;
    }
}
