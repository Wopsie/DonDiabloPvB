﻿using UnityEngine;

public enum ShieldState{
    NoShield,       //if nothing is pressed
    TapShield,      //enter when tapping button
    TapOverShield,  //enter after tapping state should be over to allow for some more precise hit detection
    HoldShield,     //enter when played holds button, always preceded by TapShieldState
};

/// <summary>
/// PlayerMovement makes player goes over track.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class NewPlayerMovement : MonoBehaviour {

    #region Singleton
    private static NewPlayerMovement instance;

    private static NewPlayerMovement GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<NewPlayerMovement>();
        }

        return instance;
    }
    #endregion
    public static NewPlayerMovement Instance { get { return GetInstance(); } }
    private int currWaypointIndex = 1;
    public int CurrWaypointIndex { get { return currWaypointIndex; } }
    [SerializeField][Range(0,50f)]
    private float maxSpeed = 25f;
    [SerializeField]
    private float speedMultiplier = 100f;
    [SerializeField]
    [Tooltip("the higher this is the move the player can move off track in corners")]
    private float movementBias = 1.2f;
    [SerializeField]
    private float lookSpeed = 10f;
    private float passingDistance;
    private Vector3 startingPos;
    [HideInInspector]
    private Rigidbody rb;
    private Vector3 velocity = new Vector3();
    public bool update = true;
    private PlayerInput pInput;
    private float tapFrames = 1f;
    [SerializeField][Tooltip("The amount of time each frame that is taken from the remaining frames. Higher number means shorter tap window")]
    private float tapCountdownPerFrame = 1;
    [HideInInspector]
    public bool tappedShield = false;
    public ShieldState currShieldState;
    public GameObject[] waypoints;

    private void OnEnable(){
        SetPlayerPosition(transform.position);
        pInput = GetComponent<PlayerInput>();
        pInput.OnPressButton += SetShieldState;
        pInput.OnHoldButton += SetShieldState;
        pInput.OnReleaseButton += SetShieldState;

        rb = GetComponent<Rigidbody>();
        GameObject[] points = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        //Debug.Log("Look For Waypoints");
        waypoints = new GameObject[points.Length];
        foreach (GameObject g in points){
            PlayerTrackingPoint p = g.GetComponent<PlayerTrackingPoint>();
            g.transform.position = new Vector3(g.transform.position.x, 0, g.transform.position.z);
            waypoints[p.PointIndex] = g;
        }

        SetPlayerPosition(new Vector3(waypoints[0].transform.position.x, 0.75f, waypoints[0].transform.position.z));

        transform.position = startingPos;
    }

    private void FixedUpdate(){
        if (update == true)
        rb.AddForce(transform.forward * (speedMultiplier * Time.deltaTime));
        float lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        rb.velocity = new Vector3(transform.forward.x * lateralSpeed, 0, transform.forward.z * lateralSpeed);
        if(rb.velocity.magnitude >= maxSpeed){
            rb.velocity *= 0.99f;
            Debug.Log("Limiting speed");
        }
        //Debug.Log(lateralSpeed);
        
        //point the current velocity in the direction of the next waypoint
    }

    private void Update() {
        TimeShieldTapState();
        //Debug.Log(currShieldState);

        //if there are no waypoints left in the list stop all movement
        if((currWaypointIndex -1) == waypoints.Length || currWaypointIndex >= waypoints.Length) {
            //rb.velocity = Vector3.zero;
            Reset();
            return;
        }
        
        //look at next waypoint
        Quaternion rot = Quaternion.LookRotation(new Vector3(waypoints[currWaypointIndex].transform.position.x, .75f, waypoints[currWaypointIndex].transform.position.z) - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, new Vector3(waypoints[currWaypointIndex].transform.position.x, 0.75f, waypoints[currWaypointIndex].transform.position.z));
        //if the player is within a certain range of the waypoint go to the next one
        if ((dist * Time.deltaTime ) * 10 <= (movementBias * Time.deltaTime) * 10) {
            currWaypointIndex++;
            passingDistance = dist;
        }
    }

    private void SetShieldState(ShieldState state){
        currShieldState = state;
        if (state == ShieldState.TapShield)
            tappedShield = true;
    }

    /// <summary>
    /// this method handles the timing at which the TapShield state is active
    /// this is necessary because the actual state is only active for 1 frame, so we have to store the state another way
    /// </summary>
    void TimeShieldTapState(){
        if(tapFrames >= 0 && tappedShield){
            //tapshield is still active
            Debug.Log("TapShield state LINGERING");
            tapFrames -= (tapCountdownPerFrame * Time.deltaTime);
        }else{
            //when this is reached it means the timer at which the TapShield state should be active has run out
            tappedShield = false;
            Debug.Log("TapShield state FINISHED");
            tapFrames = 1f;
        }
    }
    /// <summary>
    /// Makes it possible to set playermovement On/Off and saves old speed to be able to pause game.
    /// </summary>
    /// <param name="On_Off">Give string as "On" or "Off" to set movement according to given string.</param>
    public void Velocity(string On_Off)
    {
        if (On_Off == "On")
        {
            update = true;
            rb.velocity = new Vector3(velocity.x, velocity.y, velocity.z);
        }
        else if (On_Off == "Off")
        {
            update = false;
            velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.LogError("Can Only be 'On' or 'Off'");
        }
    }
    /// <summary>
    /// Sets player position to given Vector3 parameter.
    /// </summary>
    /// <param name="vec">Vector3 position to set player to.</param>
    public void SetPlayerPosition(Vector3 vec){
        startingPos = vec;
        transform.position = startingPos;
    }
    /// <summary>
    /// Resets player sets position to startpoint and set velocity to 0, next waypoint is the first 1 in array.
    /// </summary>
    public void Reset(){
        transform.position = startingPos;
        rb.velocity = Vector3.zero;
        currWaypointIndex = 1;
    }
}
