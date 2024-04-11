using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudCreatureTemporary : MonoBehaviour
{
    private CloudCreatureSpawner connectedSpawner;


    public void SetSpawner(CloudCreatureSpawner spawner)
    {
        connectedSpawner = spawner;
    }

    private void OnEnable()
    {
        StartCoroutine(KillCreature());
    }

    private IEnumerator KillCreature()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        connectedSpawner.RemoveCreature(this.gameObject);
        Destroy(this.gameObject);
    }
}
