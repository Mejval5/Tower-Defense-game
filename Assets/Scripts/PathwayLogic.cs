using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathwayLogic : MonoBehaviour
{
    public static Vector3[] Waypoints;

    void Awake()
    {
        int childrenCount = transform.childCount;
        Waypoints = new Vector3[childrenCount];

        for (int i = 0; i < childrenCount; i++)
        {
            Waypoints[i] = transform.GetChild(i).position;
        }
    }
}
