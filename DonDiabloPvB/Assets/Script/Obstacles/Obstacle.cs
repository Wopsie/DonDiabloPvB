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

    protected enum ObstacleType{
        Tap,
        Hold,
    };

    private ObstacleHelper helper;
    private new SphereCollider collider;
    protected int waypointPositionIndex = 0;
    private GameObject obstacleModel;
    private int obstacleDrawDistance = 30;
    private int obstacleAnimationTriggerDist = 20;
    private Animator anim;

    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        CheckPlayerDistances();
    }

    private void OnTriggerEnter(Collider coll){
        SnapToClosestTrackPoint(coll);
    }
    
    protected void SetInduvidualData(){
        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 5;
        collider.isTrigger = true;
        obstacleModel = transform.GetChild(0).gameObject;
        anim = obstacleModel.GetComponent<Animator>();
        obstacleModel.SetActive(false);
    }

    protected void SnapToClosestTrackPoint(Collider coll){
        if (coll.gameObject.tag == Tags.WaypointTag && waypointPositionIndex == 0){
            transform.position = new Vector3(coll.gameObject.transform.position.x, transform.position.y, coll.gameObject.transform.position.z);
            waypointPositionIndex = coll.gameObject.GetComponent<PlayerTrackingPoint>().PointIndex;
        }
    }

    protected void CheckPlayerDistances(){
        if ((waypointPositionIndex - obstacleDrawDistance) == helper.playerPassIndex){
            obstacleModel.SetActive(true);
        }else if ((waypointPositionIndex - obstacleAnimationTriggerDist) == helper.playerPassIndex){
            anim.SetBool("PlayerInRange", true);
        }
    }

    public void ReceiveHelper(ObstacleHelper obstacleHelper) {
        helper = obstacleHelper;
    }
}
