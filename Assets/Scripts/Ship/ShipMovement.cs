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
    private float _turnDirection;
    [Tooltip("How fast you accelerate.")] [SerializeField] private float _accelerationSpeed = 1;
    [Tooltip("How fast you can turn around.")] [SerializeField] private float _turnSensitivity = 1;
    [Tooltip("The maximum speed the rigidbody can have when fleeing")][SerializeField] private float maxSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_moving)
        {
            _rb.AddForce(transform.TransformDirection(_direction) * (_accelerationSpeed), ForceMode.Acceleration);
            //_rb.AddForce(transform.TransformDirection(_direction) * (_accelerationSpeed * Time.deltaTime), ForceMode.Acceleration);
            transform.Rotate(0, _turnDirection * _turnSensitivity* Time.deltaTime, 0);
            if (_rb.velocity.magnitude > maxSpeed)
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);
        }

    }

    public void OnMoveShip(InputAction.CallbackContext context)
    {
        var temp = context.ReadValue<Vector3>();
        _direction = new Vector3(0, 0, temp.z);
        _turnDirection = temp.x;
        _moving = context.performed;
    }
}
