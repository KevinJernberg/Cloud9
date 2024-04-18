using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Looks for creatures and sucks them up. At the moment it looks for colliders and not triggers - Linnéa
/// </summary>
public class SmallSuck : MonoBehaviour
{
    [SerializeField] private int maxAmountOfCreatures;
    [Tooltip("The force for the suck.")]
    [SerializeField] private float _suckForce = 10;

    [SerializeField] private float collectRadius = 0.1f;
    private List<Rigidbody> _currentlySuckedCreatures;
    private bool _sucking;

    private void Awake()
    {
        _currentlySuckedCreatures = new List<Rigidbody>();
    }

    private void Update()
    {
        if(_sucking) Suck();
    }


    public void OnSuck(InputAction.CallbackContext context)
    {
        Debug.Log("Trying to suck");
        _sucking = context.performed;
    }

    private void Suck()
    {
        for (int i = _currentlySuckedCreatures.Count - 1; i >= 0; i--)
        {
            Vector3 diff = Vector3.Normalize(_currentlySuckedCreatures[i].transform.position - transform.position);
            float dot = Vector3.Dot(diff, transform.forward);
            
            _currentlySuckedCreatures[i].AddForce(-diff * (dot * _suckForce), ForceMode.Acceleration);
            CheckIfSucked(_currentlySuckedCreatures[i]);
        }
        
    }

    private void CheckIfSucked(Rigidbody creature)
    {
        Vector3 diff = Vector3.Normalize(creature.transform.position - transform.position);
        float dot = Vector3.Dot(diff, transform.forward);
        if (Physics.Raycast(transform.position, diff*dot, out RaycastHit HitInfo, 0.15f))
        {
            if(HitInfo.transform == creature.transform)
            {
                AddToInventory(creature);
            }
        }
        //If the other way seams to unreliable use this way instead.
        /*
        List<Collider> inVicinity = Physics.OverlapSphere(transform.position, collectRadius).ToList();
        for (int i = inVicinity.Count - 1; i >= 0; i--)
        {
            if (inVicinity[i].gameObject.CompareTag("Creature"))
            {
                AddToInventory(inVicinity[i].GetComponent<Rigidbody>());
            }
        }*/
        
        
    }
    private void AddToInventory(Rigidbody creature)
    {
        CreatureInventory.AddToInventory(1);
        _currentlySuckedCreatures.Remove(creature);
        Destroy(creature.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 diff = Vector3.Normalize(other.transform.position - transform.position);
        float dot = Vector3.Dot(diff, transform.forward);
        if(dot > 0.707)
        {
            Debug.Log("in cone");
            other.TryGetComponent(out Rigidbody otherRb);
            if(otherRb != null) _currentlySuckedCreatures.Add(otherRb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.TryGetComponent(out Rigidbody otherRb);
        if(otherRb == null)return;
        if (_currentlySuckedCreatures.Contains(otherRb)) _currentlySuckedCreatures.Remove(otherRb);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, collectRadius);
        //Debug.DrawRay(transform.position, diff * dot, Color.yellow, 10f);
        
    }
}
