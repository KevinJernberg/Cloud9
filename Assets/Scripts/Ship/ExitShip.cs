using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logic for exiting the ship via a button instead of interacting again - William
/// </summary>
public class ExitShip : MonoBehaviour
{
    [SerializeField] private RudderController rudder;

    public void OnExit()
    {
        rudder._playerInput.SwitchCurrentActionMap("Player");
        rudder.player.SetParent(null);
        rudder.playerrb.isKinematic = false;
        rudder.rb.isKinematic = true;
        rudder.InteractedWith = false;
        Debug.Log("Interact exit");
    }
}
