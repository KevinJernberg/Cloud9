using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaritySimulator : MonoBehaviour
{
   /* public float CommonRate;
    public float RareRate;
    public float EpicRate;
    public float LegendaryRate;
    private float TotalRange; */

    private float RandomNumber;


    public RawImage ImageSelf;
    
    // Start is called before the first frame update
    void Start()
    {
        /*TotalRange = CommonRate + RareRate + EpicRate + LegendaryRate;
        if (TotalRange != 100)
        {
            Debug.Log("Total percentage is not 100");
        } */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateLineup(float RareRate, float EpicRate, float LegendaryRate, float TotalRange)
    {
        RandomNumber = Random.Range(1, TotalRange);

        if (RandomNumber <= LegendaryRate)
        {
            ImageSelf.color = Color.yellow;
        } else if (RandomNumber <= EpicRate)
        {
            ImageSelf.color = Color.magenta;
        } else if (RandomNumber <= RareRate)
        {
            ImageSelf.color = Color.blue;
        } else
        {
            ImageSelf.color = Color.green;
        }
    }
}
