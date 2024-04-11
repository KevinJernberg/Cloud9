using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPedistal : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject mapUI;
    public void Interact()
    {
        ToggleMap();
    }

    private void ToggleMap()
    {
        //TODO: Limit the player ability to move and look around;
        Cursor.lockState = CursorLockMode.Confined;
        mapUI.SetActive(!mapUI.activeSelf);
    }
    
}
