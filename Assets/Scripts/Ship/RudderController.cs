using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RudderController : MonoBehaviour, IInteract
{
    [SerializeField] public PlayerInput _playerInput;
    [SerializeField] public Transform player;
    [SerializeField] private Transform ship;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Rigidbody playerrb;
    [HideInInspector]public bool InteractedWith;
    public bool canDrive;

    public void Interact()
    {
        if (canDrive)
        {
            if (!InteractedWith)
            {
                _playerInput.SwitchCurrentActionMap("Ship");
                player.SetParent(ship);
                playerrb.isKinematic = true;
                rb.isKinematic = false;
                InteractedWith = true;
                Debug.Log("Interact enter");
            }
            else
            {
                _playerInput.SwitchCurrentActionMap("Player");
                player.SetParent(null);
                playerrb.isKinematic = false;
                rb.isKinematic = true;
                InteractedWith = false;
                Debug.Log("Interact exit");
            }
        }
    }
}
