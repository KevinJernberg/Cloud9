using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RudderController : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform player;
    [SerializeField] private Transform ship;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody playerrb;

    public void Interact()
    {
        _playerInput.SwitchCurrentActionMap("Ship");
        player.SetParent(ship);
        playerrb.isKinematic = true;
        rb.isKinematic = false;
        Debug.Log("Interact");
    }
}
