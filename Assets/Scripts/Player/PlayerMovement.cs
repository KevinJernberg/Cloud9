using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the players movement such as jumping, walking and changing direction. - Linnéa
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The players speed.")]
    [SerializeField] private float _speed = 1;

    [SerializeField] private float _turnSpeed = 1;
    private bool _shouldMove;
    private Vector3 _direction;
    private float _turnDirection;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(_shouldMove)rb.velocity = _direction * _speed;
        else rb.velocity = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = transform.TransformDirection(context.ReadValue<Vector3>());
        if(_direction.magnitude > 1) _direction.Normalize();
        _shouldMove = context.performed;
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        //The player should turn based on where the mouse is positioned
        _turnDirection = context.ReadValue<float>();
        transform.Rotate(0, _turnDirection * _turnSpeed/10, 0);
    }
}
