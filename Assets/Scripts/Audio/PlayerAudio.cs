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
    [SerializeField] 
    private EventReference playerFootStep;

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

    /*public void PlayerFootStepAudio(GameObject footObj, string surface)
    {
        EventInstance playerFootstepInstance = RuntimeManager.CreateInstance(playerFootstep);
        RuntimeManager.AttachInstanceToGameObject(playerFootstepInstance, footObj.transform);

        switch (surface)
        {
            case "Wood":
                playerFootstepInstance.setParameterByName("Surface", 0f);
                break;
        }

        playerFootstepInstance.start();
        playerFootstepInstance.release();
    }*/
}
