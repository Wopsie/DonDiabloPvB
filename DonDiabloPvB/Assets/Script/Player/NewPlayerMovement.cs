using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour {

    private GameObject[] waypoints;

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        Debug.Log(waypoints.Length);
    }
}
