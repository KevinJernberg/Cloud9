using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SuckMode : MonoBehaviour
{
    [SerializeField] private GameObject smallSuck;
    [SerializeField] private GameObject bigSuck;
    
    public void OnChangeSuckMode(InputAction.CallbackContext context)
    {
        Debug.Log("Updating suckmode");
        if(context.started)
        {
            
            smallSuck.SetActive(!smallSuck.activeSelf);
            bigSuck.SetActive(!bigSuck.activeSelf);
        }
    }
}
