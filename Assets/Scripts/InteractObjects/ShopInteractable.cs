using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Script for a interactable object that enables the shop - William
/// </summary>
public class ShopInteractable : MonoBehaviour, IInteract
{
    [HideInInspector]public bool InteractedWith;
    [SerializeField] public PlayerInput _playerInput;
    [SerializeField] public GameObject Shop;
    public void Interact()
    {
        if (!InteractedWith)
        {
            _playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.Confined;
            InteractedWith = true;
            Shop.SetActive(true);
            Debug.Log("Interact enter");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("Player");
            Cursor.lockState = CursorLockMode.Locked;
            InteractedWith = false;
            Shop.SetActive(false);
            Debug.Log("Interact exit");
        }
        
    }
}
