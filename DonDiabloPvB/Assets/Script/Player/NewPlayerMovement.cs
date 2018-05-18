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
    [SerializeField]
    [Tooltip("the higher this is the move the player can move off track in corners")]
    private float movementBias = 1.2f;
    private float passingDistance;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
        GameObject[] points = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        waypoints = new GameObject[points.Length];
        foreach (GameObject g in points){
            PlayerTrackingPoint p = g.GetComponent<PlayerTrackingPoint>();
            g.transform.position = new Vector3(g.transform.position.x, transform.position.y, g.transform.position.z);
            waypoints[p.PointIndex] = g;
        }
        Debug.Log(points.Length);
    }

    private void FixedUpdate(){
        rb.AddForce(transform.forward * speedMultiplier);
        float lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        rb.velocity = new Vector3(transform.forward.x * lateralSpeed, 0, transform.forward.z * lateralSpeed);
        if(rb.velocity.magnitude >= 50){
            rb.velocity = rb.velocity * maxSpeed/*(movementBias -= passingDistance)*/;
            Debug.Log("Limiting speed");
        }
        Debug.Log(lateralSpeed + " " + passingDistance);

        //add velocity to the rigidbody

        //point the current velocity in the direction of the next waypoint
    }

    private void Update()
    {
        //if there are no waypoints left in the list stop all movement
        if(waypoints.Length == currWaypointIndex)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float dist = Vector3.Distance(transform.position, waypoints[currWaypointIndex].transform.position);
        //if the player is within a certain range of the waypoint go to the next one
        if (dist <= movementBias){
            currWaypointIndex++;
            passingDistance = dist;
        }
        //look at next waypoint
        var rot = Quaternion.LookRotation(waypoints[currWaypointIndex].transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.05f);

    }
}
