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
    protected int obstacleDrawDistance = 40;
    protected int obstacleAnimationTriggerDist = 20;
    protected Animator anim;
    [SerializeField]
    protected ShieldState reqShieldState = ShieldState.NoShield;
    [SerializeField]
    protected GameObject particleParent;

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

    /// <summary>
    /// Behaviour for when player collides with obstacle
    /// </summary>
    protected virtual void OnPlayerCollision() {
        SetObstacleInactive();
        if (helper.player.currShieldState == reqShieldState){
            //success
            helper.AddScore(scoreToAward);
            Debug.Log("<color=green>VERRY G00D</color>");
            particleParent.SetActive(true);
        }else{
            //failure
            //helper.player.Reset();
            Debug.Log("<color=red>SUKKEL</color>");
            particleParent.SetActive(true);

            //check for remaining health

            //if health is not zero, display hitscreen & glitch effects

            //else
                //crash and display death screen while shader closes the windscreen
        }
    }
    
    /// <summary>
    /// Set all the data necessary for functional generic obstacle
    /// </summary>
    protected virtual void SetInduvidualData(){
        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 5;
        collider.isTrigger = true;
        obstacleModel = transform.GetChild(0).gameObject;
        anim = obstacleModel.GetComponent<Animator>();
        obstacleModel.SetActive(false);
    }

    /// <summary>
    /// empty all the containers to avoid unexpected things after player has collideded
    /// </summary>
    protected virtual void SetObstacleInactive(){
        collider.enabled = false;
        collider = null;
        obstacleModel.SetActive(false);
        anim = null;
    }

    /// <summary>
    /// Snap the obstacle to the closest tracking point to ensure it sits on the track properly
    /// </summary>
    /// <param name="coll"></param>
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
        if ((waypointPositionIndex - obstacleDrawDistance) <= helper.playerPassIndex){
            Debug.Log("REVEAL OBSTACLE");
            PlayerInRange();
        }
        if ((waypointPositionIndex - obstacleAnimationTriggerDist) == helper.playerPassIndex){
            Debug.Log("START ANIMATION");
            anim.SetBool("PlayerInRange", true);
        }
    }

    protected virtual void PlayerInRange(){
        Debug.Log("PLAYER IN RANGE");
        obstacleModel.SetActive(true);
    }

    public void ReceiveHelper(ObstacleHelper obstacleHelper) {
        helper = obstacleHelper;
    }
}
