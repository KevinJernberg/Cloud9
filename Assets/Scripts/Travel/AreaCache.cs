using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hold the information for the next area. Used when jumping from the travel map to cloud area. - Kevin
/// </summary>
public static class AreaCache
{
    private static CloudSceneStats _areaData;

    public static CloudSceneStats CloudSpawnData
    {
        set
        {
            _areaData = value;
        }
        get => _areaData;
    }
    
    
}
