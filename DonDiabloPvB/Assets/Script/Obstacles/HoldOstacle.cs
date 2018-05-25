using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOstacle : Obstacle {

    [SerializeField]
    private ObstacleType type;
    [SerializeField]
    private new ShieldState reqShieldState = ShieldState.HoldShield;

    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        base.CheckPlayerDistances();
    }

    protected override void OnPlayerCollision(){
        if(helper.player.currShieldState == reqShieldState){
            //success
            helper.AddScore(scoreToAward);
            Debug.Log("<color=green>VERRY G00D</color>");
        }else{
            helper.player.Reset();
            Debug.Log("<color=red>SUKKEL</color>");
        }
    }

    private void OnTriggerEnter(Collider coll){
        if (waypointPositionIndex == 0)
            base.SnapToClosestTrackPoint(coll);

        if (coll.gameObject.tag == Tags.PlayerTag)
            OnPlayerCollision();
    }
}
