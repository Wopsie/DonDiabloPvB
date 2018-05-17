using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour {
    [HideInInspector]
    public GameObject[] waypoints;
    private int currWaypointIndex = 1;
    public int CurrWaypointIndex { get { return currWaypointIndex; } }
    private Rigidbody rb;
    [SerializeField][Range(0,0.9f)]
    private float maxSpeed = 0.8f;
    [SerializeField]
    private float speedMultiplier = 2f;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
        GameObject[] points = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        waypoints = new GameObject[points.Length];
        foreach (GameObject g in points){
            PlayerTrackingPoint p = g.GetComponent<PlayerTrackingPoint>();
            g.transform.position = new Vector3(g.transform.position.x, transform.position.y, g.transform.position.z);
            waypoints[p.pointIndex] = g;
        }
    }

    private void FixedUpdate(){
        rb.AddForce(transform.forward * speedMultiplier);
        float lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        rb.velocity = new Vector3(transform.forward.x * lateralSpeed, 0, transform.forward.z * lateralSpeed);
        if(rb.velocity.magnitude >= 50){
            rb.velocity = rb.velocity*maxSpeed;
            Debug.Log("Limiting speed");
        }
        Debug.Log(lateralSpeed);

        //add velocity to the rigidbody

        //point the current velocity in the direction of the next waypoint

        //Debug.Log(rb.velocity);
    }

    private void Update()
    {
        if(waypoints.Length == currWaypointIndex)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        //if the player is within a certain range of the waypoint go to the next one
        //transform.position = Vector3.Lerp(transform.position, new Vector3(waypoints[currWaypointIndex].transform.position.x, transform.position.y, waypoints[currWaypointIndex].transform.position.z), 1);
        //Debug.Log(Vector3.Distance(transform.position, waypoints[currWaypointIndex].transform.position));
        if(Vector3.Distance(transform.position, waypoints[currWaypointIndex].transform.position) <= 1f){
            currWaypointIndex++;
            //Debug.Log(currWaypointIndex);
        }
        //look at next waypoint
        //transform.LookAt(new Vector3(waypoints[currWaypointIndex].transform.position.x, transform.position.y, waypoints[currWaypointIndex].transform.position.z));

        /*
        var pos : Vector3 = destination - transform.position;
        var newRot = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, speed);
        */

        var rot = Quaternion.LookRotation(waypoints[currWaypointIndex].transform.position - transform.position);
        //Debug.Log(waypoints[currWaypointIndex].transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);

        //transform.LookAt(Vector3.Lerp(transform.position, new Vector3(waypoints[currWaypointIndex].transform.position.x, transform.position.y, waypoints[currWaypointIndex].transform.position.z), 0.01f));

        //if there are no waypoints left in the list stop all movement

    }
}
