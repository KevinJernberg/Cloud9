using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Tooltip("How far you can reach.")]
    [SerializeField] private float _reachDistance;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit HitInfo, _reachDistance))
            {
                HitInfo.transform.GetComponent<IInteract>()?.Interact();
                Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * _reachDistance, Color.yellow);
            }
        }
    }
}
