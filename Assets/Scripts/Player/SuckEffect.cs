using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class SuckEffect : MonoBehaviour
{
    [Header("SuckEffect GameObject")] 
    [SerializeField]private GameObject startSuck;
    [SerializeField]private GameObject endSuck;
    [SerializeField]private GameObject activeSuck;

    private bool canStartSuck=true;
    

    public void OnSuck(InputAction.CallbackContext context)
    {
        if (context.started && canStartSuck)
        {
            StartSuck();
        }
        if (context.canceled && canStartSuck)
        {
            EndSuck();
        }
    }
    
    private void StartSuck()
    {
        startSuck.SetActive(true);
        StartCoroutine(WaitForActive());
    }
    private void EndSuck()
    {
        StopAllCoroutines();
        startSuck.SetActive(false);
        activeSuck.SetActive(false);
        endSuck.SetActive(true);
        canStartSuck = false;
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
        yield return null;
    }
}
