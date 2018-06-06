using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : Obstacle {

    private int obstacleDrawDistance = 100;
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    protected override void PlayerInRange()
    {
        Debug.Log("OPEN DEUR OFZO");
    }

}
