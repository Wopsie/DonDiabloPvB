using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldState{
    TapShield,      //enter when tapping button
    TapOverShield,  //enter after tapping state should be over to allow for some more precise hit detection
    HoldShield,     //enter when played holds button, always preceded by TapShieldState
    NoShield,       //if nothing is pressed
};

[RequireComponent(typeof(Rigidbody))]
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
    [SerializeField]
    private float lookSpeed = 10f;
    private float passingDistance;
    private PlayerInput pInput;
    private ShieldState currShieldState;

    private void Awake(){
        pInput = GetComponent<PlayerInput>();
        pInput.OnPressButton += SetShieldState;
        pInput.OnHoldButton += SetShieldState;
        pInput.OnReleaseButton += SetShieldState;

        rb = GetComponent<Rigidbody>();
        GameObject[] points = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        waypoints = new GameObject[points.Length];
        foreach (GameObject g in points){
            PlayerTrackingPoint p = g.GetComponent<PlayerTrackingPoint>();
            g.transform.position = new Vector3(g.transform.position.x, 0, g.transform.position.z);
            waypoints[p.PointIndex] = g;
        }
    }

    private void FixedUpdate(){
        rb.AddForce(transform.forward * speedMultiplier);
        float lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        rb.velocity = new Vector3(transform.forward.x * lateralSpeed, 0, transform.forward.z * lateralSpeed);
        if(rb.velocity.magnitude >= 50){
            //rb.velocity = rb.velocity * maxSpeed;
            rb.velocity *= 0.99f;
            Debug.Log("Limiting speed");
        }
        //Debug.Log(lateralSpeed);

        //add velocity to the rigidbody

        //point the current velocity in the direction of the next waypoint
    }

    private void Update() {
        //if there are no waypoints left in the list stop all movement
        if((currWaypointIndex -1) == waypoints.Length || currWaypointIndex >= waypoints.Length) {
            rb.velocity = Vector3.zero;
            return;
        }
        
        //look at next waypoint
        Quaternion rot = Quaternion.LookRotation(new Vector3(waypoints[currWaypointIndex].transform.position.x, .75f, waypoints[currWaypointIndex].transform.position.z) - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, new Vector3(waypoints[currWaypointIndex].transform.position.x, 0.75f, waypoints[currWaypointIndex].transform.position.z));
        //if the player is within a certain range of the waypoint go to the next one
        if (dist <= movementBias) {
            currWaypointIndex++;
            passingDistance = dist;
        }
    }

    private void SetShieldState(){
        //currShieldState = state;
    }
}
