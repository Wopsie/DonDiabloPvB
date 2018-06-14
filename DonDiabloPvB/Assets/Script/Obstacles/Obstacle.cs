using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obstacle base class
/// </summary>
public class Obstacle : MonoBehaviour{

    protected enum ObstacleType{
        Tap,
        Hold,
    };

    public int scoreToAward = 10;
    protected int waypointPositionIndex = 0;
    //protected ObstacleHelper ObstacleHelper.Instance;
    protected new SphereCollider collider;
    protected GameObject obstacleModel;
    protected int obstacleDrawDistance = 40;
    protected int obstacleAnimationTriggerDist = 20;
    protected Animator anim;
    [SerializeField]
    protected ShieldState reqShieldState = ShieldState.NoShield;
    [SerializeField]
    protected GameObject particleParent;
    [SerializeField]
    protected AudioSource source;
    [SerializeField]
    protected AudioClip correctClip;
    [SerializeField]
    protected AudioClip failClip;


    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        CheckPlayerDistances();
    }

    private void OnTriggerEnter(Collider coll){
        SnapToClosestTrackPoint(coll);

        if (coll.gameObject.tag == Tags.PlayerTag)
            OnPlayerCollision();
    }

    /// <summary>
    /// Behaviour for when player collides with obstacle
    /// </summary>
    protected virtual void OnPlayerCollision() {
        //if (helper.player.currShieldState == reqShieldState){
        if (ObstacleHelper.Instance.player.currShieldState == reqShieldState) {
            //success
            ObstacleHelper.Instance.AddScore(scoreToAward);
            Debug.Log("<color=green>VERRY G00D</color>");
            particleParent.SetActive(true);
            if (source != null || correctClip != null){
                source.clip = correctClip;
                source.Play();
            }
        } else {
            //failure
            //helper.player.Reset();
            Debug.Log("<color=red>SUKKEL</color>");
            particleParent.SetActive(true);
            if (source != null || correctClip != null){
                source.clip = failClip;
                source.Play();
            }

            //check for remaining health

            //if health is not zero, display hitscreen & glitch effects

            //else
            //crash and display death screen while shader closes the windscreen
        }
        SetObstacleInactive();
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
            transform.LookAt(ObstacleHelper.Instance.player.waypoints[waypointPositionIndex + 1].transform.position);
        }
    }

    /// <summary>
    /// Draw and/or animate obstacle depending on player distance
    /// </summary>
    protected virtual void CheckPlayerDistances(){
        if ((waypointPositionIndex - obstacleDrawDistance) <= ObstacleHelper.Instance.playerPassIndex){
            //Debug.Log("REVEAL OBSTACLE");
            PlayerInRange();
        }
        if ((waypointPositionIndex - obstacleAnimationTriggerDist) == ObstacleHelper.Instance.playerPassIndex){
            //Debug.Log("START ANIMATION");
            anim.SetBool("PlayerInRange", true);
        }

        /*//UNCOMMENT THIS IF PROBLEMS ARE ENCOUNTERED AS RESULT OF THE PLAYER'S COLLIDER
        if(helper.playerPassIndex == waypointPositionIndex)
        {
            OnPlayerCollision();
        }
        */
    }

    /// <summary>
    /// behaviour for when player gets within range certain range of obstacle
    /// </summary>
    protected virtual void PlayerInRange(){
        obstacleModel.SetActive(true);
    }
}