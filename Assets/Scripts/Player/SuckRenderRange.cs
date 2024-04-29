using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Raycast from the suck and sets the range the suckcameras clipping field - Linnéa
/// </summary>
public class SuckRenderRange : MonoBehaviour
{
    [SerializeField] private LayerMask SearchLayers;

    [SerializeField] private Camera SuckCam;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hit = Physics.RaycastAll(transform.position, transform.forward);
        if (hit.Length > 0)
        {
            distance = hit[0].distance; 
            if(distance is < 15f and > 2.2f)
            {
                SuckCam.farClipPlane = distance*1.25f;
            }
            else if (distance > 15f)
            {
                SuckCam.farClipPlane = 15f;
            }
        }
        
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawRay(transform.position,transform.forward*distance);
    }
}
