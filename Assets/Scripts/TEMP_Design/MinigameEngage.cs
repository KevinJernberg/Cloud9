using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameEngage : MonoBehaviour
{
    private bool MinigameIsRunning;

    
    // Start is called before the first frame update
    void Start()
    {
        MinigameIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameIsRunning)
        {

        }
    }

    public void StartMinigame()
    {
        MinigameIsRunning = true;
        //Zoom in camera? Or signal to the player that we have started the minigame in another way.
    }
}
