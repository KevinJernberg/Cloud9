using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationButtonRarity : MonoBehaviour
{
    public float CommonRate;
    public float RareRate;
    public float EpicRate;
    public float LegendaryRate;
    private float TotalRange;

    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;
    // Start is called before the first frame update
    void Start()
    {
        TotalRange = CommonRate + RareRate + EpicRate + LegendaryRate;
        if (TotalRange != 100)
        {
            Debug.Log("Total percentage is not 100");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTheColors()
    {
        Image1.GetComponent<RaritySimulator>().GenerateLineup(RareRate, EpicRate, LegendaryRate, TotalRange);
        Image2.GetComponent<RaritySimulator>().GenerateLineup(RareRate, EpicRate, LegendaryRate, TotalRange);
        Image3.GetComponent<RaritySimulator>().GenerateLineup(RareRate, EpicRate, LegendaryRate, TotalRange);
        Image4.GetComponent<RaritySimulator>().GenerateLineup(RareRate, EpicRate, LegendaryRate, TotalRange);
        Image5.GetComponent<RaritySimulator>().GenerateLineup(RareRate, EpicRate, LegendaryRate, TotalRange);
    }
}
