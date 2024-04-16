using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Manages the spawning of cloud in a cloud area.
/// Pulls information on how to build scene from the area cache. - Kevin
/// </summary>
public class CloudAreaManager : MonoBehaviour
{
    #region Variable Definitions

    private const float CLOUD_SPAWN_HEIGHT = 2f;

    [SerializeField, Tooltip("The area where cloud can spawn, represented by a cyan box gizmo")] private bool showCloudSpawnArea;
    [SerializeField] private Vector2 cloudSpawnAreaSize;
    private Rect _cloudSpawnArea;
    
    private CloudSceneStats _cloudData;

    [SerializeField] private GameObject cloudSpawnerPrefab;
    

    #endregion
    
    
    #region Gameplay Functions
    private void Start()
    {
        _cloudData = AreaCache.CloudSpawnData; // Get the stats for the current scene.
        if (_cloudData != null)
            SpawnClouds();
        else
            Debug.LogWarning($"Cloud Spawning need data to spawn clouds \n Script Name: <CloudAreaManager> \n Scene Object Name: <{gameObject.name}> ");
    }


    private void SpawnClouds()
    {
        for (int i = 0; i < _cloudData.CloudAmount; i++)
        {
            Instantiate(cloudSpawnerPrefab, RandomizeCloudPosition(), Quaternion.identity);
        }
    }

    /// <summary>
    /// Uses the area selected in the inspector to establish the borders of the randomized position.
    /// TODO: Check if clouds are to close
    /// </summary>
    /// <returns>A randomized position within the selected area</returns>
    private Vector3 RandomizeCloudPosition()
    {
        //TODO: Check if clouds are clipping
        Vector2 randomPosition = new Vector2(Random.Range(0f, _cloudSpawnArea.size.x), Random.Range(0f, _cloudSpawnArea.size.y)) + _cloudSpawnArea.position;
        
        return new Vector3(randomPosition.x, CLOUD_SPAWN_HEIGHT, randomPosition.y);
    }
    
    #endregion
    
    
    #region GUI Functions

    private void OnValidate()
    {
        _cloudSpawnArea = new Rect(new Vector2(-cloudSpawnAreaSize.x, -cloudSpawnAreaSize.y) / 2, 
            new Vector2(cloudSpawnAreaSize.x, cloudSpawnAreaSize.y)); // Sets the area to be centered at x = 0 & z = 0
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (showCloudSpawnArea)
            Gizmos.DrawCube(Vector3.up * CLOUD_SPAWN_HEIGHT, new Vector3(cloudSpawnAreaSize.x, 4, cloudSpawnAreaSize.y));
    }

    #endregion
}
