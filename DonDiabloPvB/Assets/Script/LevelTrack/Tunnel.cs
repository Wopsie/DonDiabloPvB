using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : Obstacle {

    private int obstacleDrawDistance = 100;

    private void Start()
    {
        //doe zooi
    }

    protected override void PlayerInRange()
    {
        Debug.Log("OPEN DEUR OFZO");
    }

}
