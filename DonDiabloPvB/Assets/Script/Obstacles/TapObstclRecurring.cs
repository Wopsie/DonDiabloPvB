using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObstclRecurring : Obstacle {

    [SerializeField][Tooltip("The type of obstacle, this determines the state the player must be in to successfully pass")]
    private ObstacleType type;
    private ShieldState reqShieldState = ShieldState.TapShield;
    private Animator anim;

    private void Awake(){
        SetInduvidualData();
    }

    private void Update(){
        base.CheckPlayerDistances();
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
