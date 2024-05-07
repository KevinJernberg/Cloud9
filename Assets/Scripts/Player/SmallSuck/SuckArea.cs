using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckArea : MonoBehaviour
{
    //TODO: Visual is tilted with makes for a weird feeling.
    //Behövs något som håller koll på om en creature befinner sig i denna trigger
    //Behövs något sätt att meddela 
    private List<GameObject> _inTrigger;

    private void Awake()
    {
        _inTrigger = new List<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Creature"))
        {
            Debug.Log($"{transform.name}: InArea");
            _inTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Creature")
            && _inTrigger.Contains(other.gameObject))
            _inTrigger.Remove(other.gameObject);
    }
    /*
    private void Suck()
    {
        foreach (var rb in _inTrigger)
        {
            Vector3 diff = Vector3.Normalize(rb.transform.position - _nozzlePosition.position);
            float dot = Vector3.Dot(diff, transform.forward);
            
            rb.AddForce(diff * (dot * _suckForce), ForceMode.Acceleration);
        }
        TryToRemoveCloud();
    }*/
}