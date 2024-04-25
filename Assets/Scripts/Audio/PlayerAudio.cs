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

    public EventInstance PlayerWeaponAudio(GameObject weaponObj, EventInstance playerWeaponInstance, bool weaponSuck)
    {
        switch (weaponSuck)
        {
            case true:
                playerWeaponInstance = RuntimeManager.CreateInstance(playerWeapon);
                RuntimeManager.AttachInstanceToGameObject(playerWeaponInstance, weaponObj.transform);
                playerWeaponInstance.start();
                break;
            
            case false:
                playerWeaponInstance.setParameterByName("WeaponSuck", 0f);
                playerWeaponInstance.release();
                break;
        }

        return playerWeaponInstance;
    }
}
