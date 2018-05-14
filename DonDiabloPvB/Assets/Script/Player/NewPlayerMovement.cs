using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour {

    private GameObject[] waypoints;
    private int currWaypointIndex = 1;

    private void Start(){

        GameObject[] points = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        waypoints = new GameObject[points.Length];
        foreach (GameObject g in points)
        {
            PlayerTrackingPoint p = g.GetComponent<PlayerTrackingPoint>();
            waypoints[p.pointIndex] = g;
        }
    }

    //private void FixedUpdate(){
        //add velocity to the rigidbody

        //point the current velocity in the direction of the next waypoint

    //}

    private void Update()
    {
        //if the player is within a certain range of the waypoint go to the next one
        transform.position = Vector3.Lerp(transform.position, new Vector3(waypoints[currWaypointIndex].transform.position.x, transform.position.y, waypoints[currWaypointIndex].transform.position.z), 0.15f);
        Debug.Log(Vector3.Distance(transform.position, waypoints[currWaypointIndex].transform.position));
        if(Vector3.Distance(transform.position, waypoints[currWaypointIndex].transform.position) <= 1f){
            currWaypointIndex++;
            Debug.Log(currWaypointIndex);
        }

        //look at next waypoint
        transform.LookAt(new Vector3(waypoints[currWaypointIndex].transform.position.x, transform.position.y, waypoints[currWaypointIndex].transform.position.z));

        //if there are no waypoints left in the list stop all movement

    }
}
