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
    [SerializeField] private Transform _transform;
    [SerializeField] private Camera SuckCam;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hit = Physics.RaycastAll(_transform.position, transform.forward,Mathf.Infinity ,SearchLayers);
        if (hit.Length > 0)
        {
            Debug.Log(hit[0].transform.name);
            distance = hit[0].distance; 
            if(distance is < 12f and > 1.75f)
            {
                SuckCam.farClipPlane = distance*1.25f;
            }
            else if (distance > 15f)
            {
                SuckCam.farClipPlane = 15f;
            }
        }
        else
        {
            SuckCam.farClipPlane = 15f;
        }
        
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawRay(_transform.position,transform.forward*distance);
    }
}
