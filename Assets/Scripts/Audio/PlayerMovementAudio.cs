using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.Serialization;

public class PlayerMovementAudio : MonoBehaviour
{
    public GameObject playerWalk;
    public GameObject playerRun;
    public PlayerAudio playerAudio;
    private EventInstance playerWalkInstance;
    private EventInstance playerRunInstance;
    


    public void PlayerWalkAudio()
    {
        var ray = Physics.RaycastAll(new Vector3(playerWalk.transform.position.x,playerWalk.transform.position.y+0.5f,playerWalk.transform.position.z), Vector3.down,  2);
        int temp=0;
        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].distance < ray[temp].distance)
            {
                if (ray[i].transform.tag != "Player")
                {
                    temp = i;
                }
            }
        }

        if (ray.Length>0)
        {
            playerAudio.PlayerWalkAudio(playerWalk, ray[temp].transform.tag);
        }
        
    }
    
    public void PlayerRunAudio()
    {
        var ray = Physics.RaycastAll(new Vector3(playerWalk.transform.position.x,playerWalk.transform.position.y+0.5f,playerWalk.transform.position.z), Vector3.down,  2);
        int temp=0;
        if (ray.Length > 0)
        {
            for (int i = 0; i < ray.Length; i++)
            {
                if (ray[i].distance < ray[temp].distance)
                {
                    if (ray[i].transform.tag != "Player")
                    {
                        temp = i;
                    }
                }
            }
            playerAudio.PlayerRunAudio(playerWalk, ray[temp].transform.tag);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawRay(new Vector3(playerWalk.transform.position.x,playerWalk.transform.position.y+0.5f,playerWalk.transform.position.z), 
            Vector3.down *0.5f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
