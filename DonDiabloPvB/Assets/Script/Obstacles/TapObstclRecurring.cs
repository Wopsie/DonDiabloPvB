using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObstclRecurring : Obstacle {

    [SerializeField][Tooltip("The type of obstacle, this determines the state the player must be in to successfully pass")]
    private ObstacleType type;
    private ShieldState reqShieldState = ShieldState.TapShield;

    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        base.CheckPlayerDistances();

        //Debug.Log("RECURRING REQUIRED STATE IS: " + reqShieldState);
    }

    protected override void OnPlayerCollision(){
        if(helper.player.currShieldState == reqShieldState || helper.player.tappedShield == true)
        {
            //player passes with success
            helper.AddScore(scoreToAward);
            Debug.Log("<color=green>VERRY G00D</color>");
        }else{
            //played dies
            //reset player to the start of the track for the demo
            helper.player.Reset();
            Debug.Log("<color=red>SUKKEL</color>");
        }
    }

    private void OnTriggerEnter(Collider coll){
        if(waypointPositionIndex == 0)
            base.SnapToClosestTrackPoint(coll);

        if (coll.gameObject.tag == Tags.PlayerTag)
            OnPlayerCollision();
    }
    //enable obstacle based on what waypoints the player has passed and how far that number is from the waypointPositionIndex, receive this data from ObstacleHelper
    //enable obstacleModel if player is within a certain range
    //at a smaller range start playing animation

    //obstacle can only be passed if player is in certain state when entering through the trigger
    //if player is not in this state, they die
    //obstacle itself does not change state, instead it only checks for the player's state
}
