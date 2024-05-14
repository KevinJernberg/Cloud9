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
    private EventReference playerWalk, playerRun;

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

    public void PlayerWalkAudio(GameObject walkObj, string surface)
    {
        EventInstance playerWalkInstance = RuntimeManager.CreateInstance(playerWalk);
        RuntimeManager.AttachInstanceToGameObject(playerWalkInstance, walkObj.transform);

        switch (surface)
        {
            case "Wood":
                playerWalkInstance.setParameterByName("Surface", 0f);
                break;
            case "Player":
                break;
        }

        playerWalkInstance.start();
        playerWalkInstance.release();
    }
    
    public void PlayerRunAudio(GameObject runObj, string surface)
    {
        EventInstance playerRunInstance = RuntimeManager.CreateInstance(playerRun);
        RuntimeManager.AttachInstanceToGameObject(playerRunInstance, runObj.transform);

        switch (surface)
        {
            case "Wood":
                playerRunInstance.setParameterByName("Surface", 0f);
                break;
            case "Player":
                break;
        }

        playerRunInstance.start();
        playerRunInstance.release();
    }
}
