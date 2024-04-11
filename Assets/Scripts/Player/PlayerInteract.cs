using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit HitInfo, 2f))
            {
                if(HitInfo.transform.gameObject.GetComponent<IInteract>() != null) Debug.Log($"Interacting with: {HitInfo.transform.gameObject.name}");
                Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 2.0f, Color.yellow);
            }
        }
    }
}
