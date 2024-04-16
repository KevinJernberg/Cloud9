using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Cinemachine.Utility;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Manages the spawning of creatures in clouds
/// This class also hold information about what kinds of creatures & rarities can spawn
/// Currently made for 4 rarities - Kevin
/// </summary>
public class CloudCreatureSpawner : MonoBehaviour
{
    #region Variable Declaration
    
    [Header("Gizmos")]
    [SerializeField]
    private bool showSpawnSphere;
    
    [Header("Spawn Sphere Properties")]
    [SerializeField]
    private float spawnSphereRadius;

    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    private float spawnTimer;


    [Header("Spawn Creatures & Odds")] 
    [SerializeField] private int maxCreatures;
    
    
    //TODO: Combine Creature & SpawnChance to one class, for easier rarity addition
    
    [SerializeField] private GameObject commonCreature;
    [SerializeField, Range(0f, 1f)] private float commonSpawnChance = 0.25f;
    
    [SerializeField] private GameObject rareCreature;
    [SerializeField, Range(0f, 1f)] private float rareSpawnChance = 0.25f;
    
    [SerializeField] private GameObject epicCreature;
    [SerializeField, Range(0f, 1f)] private float epicSpawnChance = 0.25f;
    
    [SerializeField] private GameObject legendaryCreature;
    [SerializeField, Range(0f, 1f)] private float legendarySpawnChance;


    
    private List<GameObject> _spawnedCreatures = new List<GameObject>();
    private float SpawnChangeCombined => commonSpawnChance + rareSpawnChance + epicSpawnChance + legendarySpawnChance;

    #endregion


    #region Gameplay Functions
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            if (_spawnedCreatures.Count < maxCreatures) // Adhere the creature count to the requested amount
                SpawnCreature();
            spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }


    /// <summary>
    /// Manages the randomization of a creature
    /// This does not spawn the creature
    /// </summary>
    /// <returns>A creature of randomized rarity</returns>
    private GameObject RandomizeCreature()
    {
        float randomValue = Random.Range(0f, 1f);
        
        
        // Chooses rarity based upon randomValue
        if (randomValue < legendarySpawnChance) // Legendary rarity
            return legendaryCreature;
        else if (randomValue < epicSpawnChance + legendarySpawnChance)  // Epic rarity
            return epicCreature;
        else if (randomValue < rareSpawnChance + epicSpawnChance + legendarySpawnChance)  // Rare rarity
            return rareCreature;
        else  // Common rarity
            return commonCreature;

    }
    
    /// <summary>
    /// Spawns a randomized creature. Adding relevant references in the process
    /// Randomization process done in other function
    /// </summary>
    private void SpawnCreature()
    {
        GameObject creature = Instantiate(RandomizeCreature(), 
            transform.position + Random.insideUnitSphere.ProjectOntoPlane(Vector3.up) * spawnSphereRadius, // ProjectOntoPlane makes every creature spawn on same y level.
            Quaternion.identity);
        
        creature.GetComponent<CloudCreatureTemporary>().SetSpawner(this);
        _spawnedCreatures.Add(creature);
    }
    
    public void RemoveCreature(GameObject creature)
    {
        _spawnedCreatures.Remove(creature);
    }
    
    #endregion
    
    
    #region GUI Functions

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showSpawnSphere)
            Gizmos.DrawSphere(transform.position, spawnSphereRadius);
    }

    private void OnValidate()
    {
        minSpawnDelay = Mathf.Max(0, minSpawnDelay);
        maxSpawnDelay = Mathf.Max(minSpawnDelay, maxSpawnDelay);
        
        // Make all changes add upp to 1. Thus making change calculation less expensive later
        commonSpawnChance /= SpawnChangeCombined;
        rareSpawnChance /= SpawnChangeCombined;
        epicSpawnChance /= SpawnChangeCombined;
        legendarySpawnChance /= SpawnChangeCombined;
    }
#endif
    
    #endregion
}



