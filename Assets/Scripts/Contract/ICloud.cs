using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICloud
{
    public struct SpawnTable
    {
        public GameObject Prefab;
        public float spawnOdds;
    }

    List<SpawnTable> SpawnAble { get; set; }
    int AmountSpawned { get; set; }
    void Spawn(List<SpawnTable> spawnStats, out GameObject obj);
}

public class Cloud
{
    void function()
    {
        
        //gg.spawnOdds / 
    }
}
