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
    [Tooltip("The max amount of creatures that the player can store.")]
    [SerializeField] private int maxAmountOfCreatures;
    [Tooltip("The force for the suck.")]
    [SerializeField] private float _suckForce = 10;
    private List<Rigidbody> _currentlySuckedCreatures;
    private bool _sucking;

    private void Awake()
    {
        _currentlySuckedCreatures = new List<Rigidbody>();
        CreatureInventory.SetMaxInventorySpace(maxAmountOfCreatures);
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

    /// <summary>
    /// Checks the list creatures in the colliderlist. If not 0 it adds a force on the creature towards the suck.
    /// Then it calls a function to check if it can be sucked. Goes through the list backwards to make manipulation
    /// of the list possible.
    /// </summary>
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
    /// <summary>
    /// Checks if a specific creature is close enough to the suck to add to the inventory. Raycasts towards the
    /// specific creature, if hit by the raycast it is close enough.
    /// </summary>
    /// <param name="creature">The rigidbody of the creature to check.</param>
    private void CheckIfSucked(Rigidbody creature)
    {
        Vector3 diff = Vector3.Normalize(creature.transform.position - transform.position);
        float dot = Vector3.Dot(diff, transform.forward);
        if (Physics.Raycast(transform.position, diff*dot, out RaycastHit HitInfo, 0.2f))
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
    /// <summary>
    /// If the inventory is not full it adds the creature to the inventory, removes it from the list and
    /// destroys the GameObject.
    /// </summary>
    /// <param name="creature">The rigidbody of the creature to add.</param>
    private void AddToInventory(Rigidbody creature)
    {
        if(CreatureInventory.AddToInventory(1))
        {
            _currentlySuckedCreatures.Remove(creature);
            creature.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// If the creature is inside the collider it checks if it is actually inside the cone, if yes, it adds
    /// it to the list of creatures that can be sucked, otherwise it checks if it is in list of creature to suck
    /// if so, it removes it.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        //This line can be removed when we fix collision layers and decide how to structure it.
        if(!other.CompareTag("Creature"))return;
        
        Vector3 diff = Vector3.Normalize(other.transform.position - transform.position);
        float dot = Vector3.Dot(diff, transform.forward);
        other.TryGetComponent(out Rigidbody otherRb);
        if(dot > 0.707 && !_currentlySuckedCreatures.Contains(otherRb))
        {
            Debug.Log("in cone");
            
            if(otherRb != null) _currentlySuckedCreatures.Add(otherRb);
        }
        else
        {
            if(_currentlySuckedCreatures != null && _currentlySuckedCreatures.Contains(otherRb))_currentlySuckedCreatures.Remove(otherRb);
        }
    }
    /// <summary>
    /// If the creature leaves the collision box it is removed from creatures to suck.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.gameObject.CompareTag("Creature")) return;
        other.gameObject.TryGetComponent(out Rigidbody otherRb);
        if(otherRb == null)return;
        if (_currentlySuckedCreatures.Contains(otherRb)) _currentlySuckedCreatures.Remove(otherRb);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
        //Debug.DrawRay(transform.position, diff * dot, Color.yellow, 10f);
        
    }
}
