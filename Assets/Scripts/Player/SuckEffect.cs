using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using FMOD.Studio;
public class SuckEffect : MonoBehaviour
{
    [Header("SuckEffect GameObject")] 
    [SerializeField]private GameObject startSuck;
    [SerializeField]private GameObject endSuck;
    [SerializeField]private GameObject activeSuck;

    [Header("Audio")] 
    public GameObject weaponAudio;
    public PlayerAudio playerAudio;
    private EventInstance playerWeaponInstance;

    private bool canStartSuck=true;
    private bool inEndAnim;
    private bool DontSuck;
    

    public void OnSuck(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inEndAnim)
            {
                DontSuck = true;
            }
            else
            {
                DontSuck = false;
            }
            
            if (canStartSuck)
            {
                StartSuck();
            }
        }
        if (context.canceled && canStartSuck && !DontSuck)
        {
            EndSuck();
        }
    }
    
    private void StartSuck()
    {
        startSuck.SetActive(true);
        StartCoroutine(WaitForActive());
        playerWeaponInstance = playerAudio.PlayerWeaponAudio(weaponAudio, playerWeaponInstance, true);
        //if (!playerWeaponInstance.isValid())
            

    }
    private void EndSuck()
    {
        StopAllCoroutines();
        startSuck.SetActive(false);
        activeSuck.SetActive(false);
        endSuck.SetActive(true);
        canStartSuck = false;
        inEndAnim = true;
        if (playerWeaponInstance.isValid())
            playerAudio.PlayerWeaponAudio(weaponAudio, playerWeaponInstance, false);
        StartCoroutine(DisableEndSuckAfterDuration());
        
    }

    IEnumerator WaitForActive()
    {
        yield return new WaitForSeconds(1);
        startSuck.SetActive(false);
        activeSuck.SetActive(true);
        yield return null;
    }
    IEnumerator DisableEndSuckAfterDuration()
    {
        yield return new WaitForSeconds(0.7f);
        endSuck.SetActive(false);
        canStartSuck = true;
        yield return new WaitForEndOfFrame();
        inEndAnim = false;
        yield return null;
    }
}
