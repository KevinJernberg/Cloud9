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

    private float startSuckDuration;

    public void OnSuck(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartSuck();
        }
        if (context.canceled)
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
        yield return new WaitForSeconds(1.5f);
        endSuck.SetActive(false);
        yield return null;
    }
}
