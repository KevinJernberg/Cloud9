using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigSuck : MonoBehaviour
{
    ///Det ska finnas en modell som man bär på, det ska gå att switcha mellan smallsuck och bigsuck
    /// Smallsuck och bigsuck ska ha samma knappar för input
    /// Den ska suga upp moln som är inom en viss radie, när ett moln sugs upp så ska creatures spawnas.

    [Tooltip("The force for the suck.")]
    [SerializeField] private float _suckForce = 10;

    private bool _sucking;
    
    private List<Rigidbody> _inTrigger;

    private void Awake()
    {
        _inTrigger = new List<Rigidbody>();
    }

    private void Update()
    {
        if(_sucking)Suck();
    }

    public void OnSuck(InputAction.CallbackContext context)
    {
        if(!gameObject.activeSelf)return;
        _sucking = context.performed;
    }
    
    private void Suck()
    {
        foreach (var rb in _inTrigger)
        {
            Vector3 diff = Vector3.Normalize(rb.transform.position - transform.position);
            float dot = Vector3.Dot(diff, transform.forward);
            
            rb.AddForce(-diff * (dot * _suckForce), ForceMode.Acceleration);
        }
        TryToRemoveCloud();
    }

    private void TryToRemoveCloud()
    {
        foreach (var rb in _inTrigger)
        {
            Vector3 diff = Vector3.Normalize(rb.transform.position - transform.position);
            float dot = Vector3.Dot(diff, transform.forward);
            if (Physics.Raycast(transform.position, diff*dot, out RaycastHit HitInfo, 0.2f))
            {
                if(HitInfo.transform == rb.transform)
                {
                    Destroy(rb.gameObject);
                    //SpawnCreatures
                    Debug.Log("SpawnCreature.");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Cloud"))return;
        
        if(other.TryGetComponent(out Rigidbody rb))_inTrigger.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Cloud"))return;
        
        if(other.TryGetComponent(out Rigidbody rb))_inTrigger.Remove(rb);
    }
}
