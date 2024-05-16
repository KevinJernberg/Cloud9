using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReturnToHubPedestal : MonoBehaviour, IInteract
{
    private const string HUB_SCENE_NAME = "Hub";
    
    [SerializeField] private GameObject mapUI;
    [SerializeField] private PlayerInput _playerInputComponent;
    
    [Header("Audio")] 
    public UIAudio uIAudio;
    
    public void Interact()
    {
        ToggleMap();
    }

    private void ToggleMap()
    {
        //TODO: Limit the player ability to move and look around;
        Cursor.lockState = CursorLockMode.None;
        mapUI.SetActive(!mapUI.activeSelf);
        _playerInputComponent.SwitchCurrentActionMap(mapUI.activeSelf ? "UI" : "Player");
        
        uIAudio.MapToggleAudio(transform);
    }

    public void OnCloseMap(InputAction.CallbackContext context)
    {
        if(mapUI.activeSelf)
        {
            if (context.started)
            {
                CloseMap();
            }
        }
    }
    public void CloseMap()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mapUI.SetActive(false);
        _playerInputComponent.SwitchCurrentActionMap("Player");
        uIAudio.MapCloseAudio(transform);
    }

    public void ReturnToHub()
    {

        SceneManager.LoadScene(HUB_SCENE_NAME);
    }
    
}
