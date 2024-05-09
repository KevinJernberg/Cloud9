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

    // Start is called before the first frame update
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
}
