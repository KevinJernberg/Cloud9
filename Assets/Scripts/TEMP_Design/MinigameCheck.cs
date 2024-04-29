using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameCheck : MonoBehaviour
{
   
    private float CurrentPoints;

    public GameObject Creature;
    public float rayMaxRange;
    public float MaxPoints;
    public float StartPoints;
    public float MaxTime;
    private float Timepassed;

    public float Zone1;
    public float Zone2;
    public float Zone3;
    public float Zone4;
    void Update()
    {
        Timepassed += Time.deltaTime;

        if (Timepassed >= MaxTime)
        {
            Timepassed = 0;
            PointCheck();
        }


    }

    public void PointCheck()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 DistanceToCreature = Vector3.Normalize(Creature.transform.position - transform.position);

        if (Vector3.Dot(forward, DistanceToCreature) > 0.95)
        {
            CurrentPoints += Zone1;
        }
        else if (Vector3.Dot(forward, DistanceToCreature) <= 0.95 && Vector3.Dot(forward, DistanceToCreature) > 0.9)
        {
            CurrentPoints += Zone2;
        }
        else if (Vector3.Dot(forward, DistanceToCreature) <= 0.9 && Vector3.Dot(forward, DistanceToCreature) > 0.80)
        {
            CurrentPoints += Zone3;
        }
        else
        {
            CurrentPoints += Zone4;
        }

        Creature.gameObject.GetComponent<CaptureProgress>().UpdateSlider(CurrentPoints, MaxPoints);
      
        if (CurrentPoints >= MaxPoints)
            {
                Debug.Log("You captured it, epic win");
            }
            else if (CurrentPoints <= 0)
            {
                Debug.Log("You lose");
                //Destroy(hit.transform.parent.gameObject);
            }
        
        
    }

}
