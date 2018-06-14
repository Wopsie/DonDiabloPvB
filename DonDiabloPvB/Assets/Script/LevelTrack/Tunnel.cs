﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class inherid from Obstacle class.
/// </summary>
public class Tunnel : Obstacle {

    //private int obstacleDrawDistance = 10;
    private Animator animator;

   
    /// <summary>
    /// Check distance between player and end of tunnel and if in range perform open tunnel door animation.
    /// </summary>
    protected override void CheckPlayerDistances()
    {
        print(waypointPositionIndex + " waypointpostionindex");
        print(ObstacleHelper.Instance + " helper");
        if ((waypointPositionIndex - 10) <= ObstacleHelper.Instance.playerPassIndex)
        {
            //Debug.Log("Play Tunnel animation");
            animator.SetBool("TunnelOpen", true);

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
