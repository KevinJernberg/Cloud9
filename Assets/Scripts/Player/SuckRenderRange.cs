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
        Debug.Log("do");
        var hit = Physics.RaycastAll(transform.position, Vector3.forward, SearchLayers);
        Debug.Log(hit.Length);
        if (hit.Length > 0)
        {
            
            distance = hit[0].distance; 
        }
        
        //if(hit[0].distance)
        //SuckCam.farClipPlane
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        //Gizmos.DrawRay(transform.position,transform.);
    }
}
