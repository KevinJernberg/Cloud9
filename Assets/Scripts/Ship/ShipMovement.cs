using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the ships movement. - Linnéa, William
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
    [Tooltip("The minimum speed the rigidbody can have when fleeing")][SerializeField] private float minSpeed;
    
    [Header("Temporary")]
    [Tooltip("If True the ship will only be able to turn when it is driven forward")][SerializeField] private bool onlyTurnWhenDrive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_moving)
        {
            transform.Rotate(0, _turnDirection * _turnSensitivity* Time.deltaTime, 0);
            _rb.AddForce(transform.TransformDirection(_direction) * (_accelerationSpeed*maxSpeed), ForceMode.Acceleration);
            Debug.Log(_rb.velocity.magnitude);
            if (_rb.velocity.magnitude > maxSpeed)
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxSpeed);
            if (_rb.velocity.magnitude < minSpeed)
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, minSpeed);
        }

    }

    public void OnMoveShip(InputAction.CallbackContext context)
    {
        var temp = context.ReadValue<Vector3>();
        _direction = new Vector3(0, 0, temp.z);
        if (onlyTurnWhenDrive)
        {
            if (temp.z == 0f)
            {
                _turnDirection = 0f;
            }
            else
            {
                _turnDirection = temp.x;
            }
        }
        else
        {
            _turnDirection = temp.x;
        }
        
        
        _moving = !context.canceled;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(_direction)*100);
        

    }
}
