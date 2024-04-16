using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handles how the camera rotates in order to look up and down. - Linnéa
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Tooltip("How fast you can look up and down.")]
    [SerializeField] private float _turnSensitivity = 1;
    [Tooltip("How far up you can look.")]
    [SerializeField] private float _rotationMin = -85f;
    [Tooltip("How far down you can look.")]
    [SerializeField] private float _rotationMax = 85f;
    private float _turnDirection;
    private float _cameraRotation;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    /// <summary>
    /// Input function called by the PlayerInput component. Rotates the camera to make the player look up and down.
    /// </summary>
    /// <param name="context">Read from the unity event.</param>
    public void OnTurn(InputAction.CallbackContext context)
    {
        _cameraRotation += context.ReadValue<float>() * _turnSensitivity / 10;
        _cameraRotation = Mathf.Clamp(_cameraRotation, _rotationMin, _rotationMax);
        _mainCamera.transform.localEulerAngles = new Vector3(_cameraRotation, _mainCamera.transform.localEulerAngles.y,
            _mainCamera.transform.localEulerAngles.z);
    }
}
