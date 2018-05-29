using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obstacle base class
/// </summary>
public class Obstacle : MonoBehaviour {

    //obstacles share certain properties, this class allows us to set defaults for every obstacle if we are not sure about certain unique details
    //every obstacle has a type
    //every obstacle has access to ObstacleHelper (still thinking about optimal way to do this)
    //every obstacle has particle when they get destroyed
    //every obstacle has sound effect for destroy
    //every obstacle has particle/animation for "initiation"
    //every obstacle has colour shift as player get closer (?)

    protected enum ObstacleType {
        Tap,
        Hold,
    };

    public int scoreToAward = 10;
    protected int waypointPositionIndex = 0;
    protected ObstacleHelper helper;
    protected new SphereCollider collider;
    protected GameObject obstacleModel;
    protected int obstacleDrawDistance = 10;
    protected int obstacleAnimationTriggerDist = 11;
    protected Animator anim;
    [SerializeField]
    protected ShieldState reqShieldState = ShieldState.NoShield;

    private void Awake() {
        SetInduvidualData();
    }

    private void Update() {
        CheckPlayerDistances();

        //add gameplay functionality

        //upon passing check what state player is in ith input

        //if player matches the state required to pass
        //add score & destroy obstacle
        //else
        //Reset player "die"
    }

    private void OnTriggerEnter(Collider coll) {
        SnapToClosestTrackPoint(coll);

        if (coll.gameObject.tag == Tags.PlayerTag)
            OnPlayerCollision();
    }

    protected virtual void OnPlayerCollision() { }
    
    protected virtual void SetInduvidualData(){
        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 5;
        collider.isTrigger = true;
        obstacleModel = transform.GetChild(0).gameObject;
        anim = obstacleModel.GetComponent<Animator>();
        obstacleModel.SetActive(false);
    }

    protected void SnapToClosestTrackPoint(Collider coll){
        if (coll.gameObject.tag == Tags.WaypointTag && waypointPositionIndex == 0){
            //Snap position
            transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y, coll.gameObject.transform.position.z);
            waypointPositionIndex = coll.gameObject.GetComponent<PlayerTrackingPoint>().PointIndex;
            //rotate
            transform.LookAt(helper.player.waypoints[waypointPositionIndex + 1].transform.position);
        }
    }

    /// <summary>
    /// Draw and/or animate obstacle depending on player distance
    /// </summary>
    protected void CheckPlayerDistances(){
        Debug.Log(waypointPositionIndex - obstacleDrawDistance + " " + helper.playerPassIndex + " " + waypointPositionIndex);
        if ((waypointPositionIndex - obstacleDrawDistance) <= helper.playerPassIndex){
            obstacleModel.SetActive(true);
        }else if ((waypointPositionIndex - obstacleAnimationTriggerDist) == helper.playerPassIndex){
            anim.SetBool("PlayerInRange", true);
            Debug.Log("START ANIMATION");
        }
    }

    public void ReceiveHelper(ObstacleHelper obstacleHelper) {
        helper = obstacleHelper;
    }
}
