using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// This MonoBehavior sits on the icons that spawn on the UI Travel Map.
/// When icon is pressed the scene changes. - Kevin
/// </summary>
public class CloudIcon : MonoBehaviour
{
    #region Variable Declaration

    private const string CLOUD_AREA_SCENE_NAME = "KevinCloudArea";
    
    private CloudSceneStats _areaData;
    
    //Components
    [SerializeField] private TextMeshProUGUI cloudAmountText;

    #endregion

    
    #region Gameplay Functions
    private void Start()
    {
        _areaData = new CloudSceneStats(Random.Range(1, 10));
        cloudAmountText.text = $"{_areaData.CloudAmount}";
    }
    
    public void ChangeScene()
    {
        AreaCache.CloudSpawnData = _areaData;
        SceneManager.LoadScene(CLOUD_AREA_SCENE_NAME);
    }
    
    #endregion
}
