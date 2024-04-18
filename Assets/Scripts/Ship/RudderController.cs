using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RudderController : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerInput _playerInput;

    public void Interact()
    {
        _playerInput.SwitchCurrentActionMap("Ship");
        Debug.Log("Interact");
    }
}
