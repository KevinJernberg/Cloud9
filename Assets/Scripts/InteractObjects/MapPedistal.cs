using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapPedistal : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject mapUI;
    [SerializeField] private PlayerInput _playerInputComponent;
    public void Interact()
    {
        ToggleMap();
    }

    private void ToggleMap()
    {
        //TODO: Limit the player ability to move and look around;
        Cursor.lockState = CursorLockMode.Confined;
        mapUI.SetActive(!mapUI.activeSelf);
        _playerInputComponent.SwitchCurrentActionMap(mapUI.activeSelf ? "UI" : "Player");
    }

    public void OnCloseMap(InputAction.CallbackContext context)
    {
        if(mapUI.activeSelf)
        {
            if (context.started)
            {
                Cursor.lockState = CursorLockMode.Locked;
                mapUI.SetActive(false);
                _playerInputComponent.SwitchCurrentActionMap("Player");
            }
        }
    }
    
}
