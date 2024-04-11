using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 1;
    private float _turnDirection;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        //The player should turn based on where the mouse is positioned
        _turnDirection = context.ReadValue<float>();
        mainCamera.transform.Rotate(_turnDirection * _turnSpeed/10, 0, 0);
    }
}
