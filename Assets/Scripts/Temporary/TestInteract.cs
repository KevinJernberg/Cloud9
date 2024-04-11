using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteract
{
    public void Interact()
    {
        Debug.Log("Intercting");
    }
}
