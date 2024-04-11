using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _turnSensitivity = 1;
    [SerializeField] private float _rotationMin = -85f;
    [SerializeField] private float _rotationMax = 85f;
    private float _turnDirection;
    private float _cameraRotation;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        //The player should turn based on where the mouse is positioned
        _cameraRotation += context.ReadValue<float>() * _turnSensitivity / 10;
        _cameraRotation = Mathf.Clamp(_cameraRotation, _rotationMin, _rotationMax);
        mainCamera.transform.localEulerAngles = new Vector3(_cameraRotation, mainCamera.transform.localEulerAngles.y,
            mainCamera.transform.localEulerAngles.z);
        //mainCamera.transform.Rotate(_turnDirection * _turnSpeed/10, 0, 0);
    }
}
