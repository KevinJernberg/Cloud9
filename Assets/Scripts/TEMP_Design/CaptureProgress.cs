using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureProgress : MonoBehaviour
{
    public Slider Progressbar;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        Debug.Log(Progressbar.value);
    }
    // Update is called once per frame


    public void UpdateSlider(float CurrentPoints, float MaxPoints)
    {
        
        Progressbar.value = CurrentPoints / MaxPoints;
    }
}
