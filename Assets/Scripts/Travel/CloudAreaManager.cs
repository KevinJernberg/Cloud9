using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField, Tooltip("The ships transform, used to not spawn clouds top of it")] private Transform ship;
    [SerializeField] private Vector2 cloudSpawnAreaSize;
    [SerializeField] private float minimumCloudDistance;
    private Rect _cloudSpawnArea = new Rect();
    
    private CloudSceneStats _cloudData;


    [SerializeField] private GameObject[] clouds;
    

    #endregion
    
    
    #region Gameplay Functions
    private void Start()
    {
        _cloudData = AreaCache.CloudSpawnData; // Get the stats for the current scene.
        
        SetNewCloudArea();
        
        if (_cloudData != null)
            SpawnClouds();
        else
            Debug.LogWarning($"Cloud Spawning need data to spawn clouds \n Script Name: <CloudAreaManager> \n Scene Object Name: <{gameObject.name}> ");
    }


    private void SpawnClouds()
    {
        for (int i = 0; i < _cloudData.CloudAmount; i++)
        {
            bool foundCloudPosition = false;
            Vector3 randomizedCloudPosition = RandomizeCloudPosition();
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < clouds.Length; k++)
                {
                    foundCloudPosition = Vector3.Distance(randomizedCloudPosition, clouds[k].transform.position) >
                                         minimumCloudDistance;
                    if (foundCloudPosition)
                        break;
                }
                foundCloudPosition = Vector3.Distance(randomizedCloudPosition,ship.position) > minimumCloudDistance;
                if (foundCloudPosition)
                    break;
            }
            
            if (foundCloudPosition)
                Instantiate(clouds[Random.Range(0, clouds.Length)], RandomizeCloudPosition(), Quaternion.identity);
            else
                Debug.LogWarning("Cloud could not spawn");
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
    
    private void SetNewCloudArea()
    {
        _cloudSpawnArea = new Rect(new Vector2(-cloudSpawnAreaSize.x, -cloudSpawnAreaSize.y) / 2, 
            new Vector2(cloudSpawnAreaSize.x, cloudSpawnAreaSize.y)); // Sets the area to be centered at x = 0 & z = 0
    }
    
    #endregion
    
    
    #region GUI Functions
    
#if UNITY_EDITOR    
    private void OnValidate()
    {
        SetNewCloudArea();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (showCloudSpawnArea)
            Gizmos.DrawCube(Vector3.up * CLOUD_SPAWN_HEIGHT, new Vector3(cloudSpawnAreaSize.x, 4, cloudSpawnAreaSize.y));
    }
#endif
    
    #endregion
}
