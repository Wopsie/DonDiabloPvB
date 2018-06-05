using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObstacle : Obstacle {

    [SerializeField]
    private ObstacleType type;
    private ShieldState reqShieldState = ShieldState.HoldShield;

    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        base.CheckPlayerDistances();
    }

    private void OnTriggerEnter(Collider coll){
        if (waypointPositionIndex == 0)
            base.SnapToClosestTrackPoint(coll);

        if (coll.gameObject.tag == Tags.PlayerTag)
            OnPlayerCollision();
    }
}