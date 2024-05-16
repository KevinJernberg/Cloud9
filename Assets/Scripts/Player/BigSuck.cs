using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the big suck. Looks for clouds, all clouds close enough is sucked towards the big suck.
/// If clouds are close enough they are destroyed. - Linnéa
/// </summary>
public class BigSuck : MonoBehaviour
{
    ///Det ska finnas en modell som man bär på, det ska gå att switcha mellan smallsuck och bigsuck
    /// Smallsuck och bigsuck ska ha samma knappar för input
    /// Den ska suga upp moln som är inom en viss radie, när ett moln sugs upp så ska creatures spawnas.

    [Tooltip("The force for the suck.")]
    [SerializeField] private float _suckForce = 10;

    [Tooltip("The nozzle o the big gun, the point where clouds are sucked towards.")]
    [SerializeField] private Transform _nozzlePosition;

    [Tooltip("The point where the cloud counts as sucked.")]
    [SerializeField] private float _endSuckRange = 0.2f;

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
        Debug.Log("Sucking");
        _sucking = context.performed;
    }
    /// <summary>
    /// Adds a force towards the suck, then tries to remove them.
    /// </summary>
    private void Suck()
    {
        foreach (var rb in _inTrigger)
        {
            Vector3 diff = Vector3.Normalize(_nozzlePosition.position - rb.transform.position);
            float dot = Mathf.Max(0, Vector3.Dot(diff, transform.up * -1)); //TODO: The transform is rotated 90 degrees in the x angle, thus the forward goes down and the up is the correct "forward angle". Change this to forward if the angle is changed
            
            Debug.Log($"{diff} \n {dot}");
            rb.AddForce(diff * (dot * _suckForce), ForceMode.Acceleration);
        }
        TryToRemoveCloud();
    }

    /// <summary>
    /// If cloud is close enough it is destroyed(sucked).
    /// </summary>
    private void TryToRemoveCloud()
    {
        for (int i = _inTrigger.Count -1; i >= 0; i--)
        {
            Rigidbody rb = _inTrigger[i];
            Vector3 diff = Vector3.Normalize(rb.transform.position - _nozzlePosition.position);
            float dot = Vector3.Dot(diff, transform.forward);
            if (Physics.Raycast(_nozzlePosition.position, -diff*dot, out RaycastHit HitInfo, _endSuckRange))
            {
                if(HitInfo.transform == rb.transform)
                {
                    _inTrigger.Remove(rb);
                    Destroy(rb.gameObject);
                    
                    Debug.Log("SpawnCreature.");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Cloud"))return;
        Debug.Log("InCcone");
        if(other.TryGetComponent(out Rigidbody rb))_inTrigger.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Cloud"))return;
        Debug.Log("NotInCcone");
        if(other.TryGetComponent(out Rigidbody rb) && _inTrigger.Contains(rb))_inTrigger.Remove(rb);
    }

}
