using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(menuName = "Scriptables/Audio/Player")]

public class PlayerAudio : ScriptableObject
{
    [SerializeField] 
    private EventReference playerWeapon;
}
