using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : Obstacle {

    //private int obstacleDrawDistance = 10;
    private Animator animator;

   

    protected override void CheckPlayerDistances()
    {
        print(waypointPositionIndex + " waypointpostionindex");
        print(helper + " helper");
        if ((waypointPositionIndex - 10) <= helper.playerPassIndex)
        {
            Debug.Log("Play Tunnel animation");

        }
    }

    protected override void SetInduvidualData()
    {
        animator = gameObject.GetComponent<Animator>();


    }
    protected override void PlayerInRange()
    {
        Debug.Log("OPEN DEUR OFZO");
    }

}
