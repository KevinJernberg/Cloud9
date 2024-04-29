using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameEngage : MonoBehaviour
{
    private bool MinigameIsRunning;
    public List<Transform> checkpoints;
    private Transform CurrentGoal;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        MinigameIsRunning = true;
        CurrentGoal = checkpoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameIsRunning)
        {

            if (transform.position == CurrentGoal.position)
            {
                NextCheckpoint();
            }
            
          transform.position =  Vector3.MoveTowards(transform.position, CurrentGoal.position, speed * Time.deltaTime);
        }
    }

    public void StartMinigame()
    {
        MinigameIsRunning = true;
        //Zoom in camera? Or signal to the player that we have started the minigame in another way.
    }

    public void NextCheckpoint()
    {
        int CurrentCheckpointNumber = Random.Range(0, checkpoints.Count);
        CurrentGoal = checkpoints[CurrentCheckpointNumber];
    }
}
