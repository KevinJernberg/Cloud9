using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the ships movement. - Linnéa
/// </summary>
public class ShipMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _moving;
    private Vector3 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_moving)
        {
            _rb.AddForce(transform.TransformDirection(_direction)*0.2f, ForceMode.Force);
        }
    }

    public void OnMoveShip(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector3>();
        _moving = context.performed;
    }
}
