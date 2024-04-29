using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameCheck : MonoBehaviour
{
    [SerializeField] private LayerMask creaturemask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMouseClick()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward,out hit, 10f, creaturemask);
        if (hit.transform.gameObject != null)
        {
         
        }
    }
}
