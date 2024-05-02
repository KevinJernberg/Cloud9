using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(menuName = "Scriptables/Audio/UI")]
public class UIAudio : ScriptableObject
{
    [SerializeField] 
    private EventReference mapToggle;
    [SerializeField] 
    private EventReference mapClose;
    [SerializeField] 
    private EventReference mapSceneChange;
    
    public void MapToggleAudio(Transform mapToggleTransform)
    {
        RuntimeManager.PlayOneShot(mapToggle, mapToggleTransform.position);
    }

    public void MapCloseAudio(Transform mapCloseTransform)
    {
        RuntimeManager.PlayOneShot(mapClose, mapCloseTransform.position);
    }

    public void MapSceneChangeAudio(Transform mapSceneChangeTransform)
    {
        RuntimeManager.PlayOneShot(mapSceneChange, mapSceneChangeTransform.position);
    }
}
