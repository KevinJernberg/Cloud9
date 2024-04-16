using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A small container for the stats that a cloudArea should have.
/// This is a container, it does not create any values.
/// For now it is simple but can include all types of data. - Kevin
/// </summary>
public class CloudSceneStats
{
    public CloudSceneStats(int cloudAmount)
    {
        CloudAmount = cloudAmount;
    }
    
    public int CloudAmount;
}
