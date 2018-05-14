using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {
    private int disToPlayer;
    private bool IsDestroyed;
    private Vector3 playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag(Tags.PlayerTag).transform.position;

    }
   
     private int GetDistanceToPlayer()
    {
        float dis = transform.position.z - playerPos.z;
        return 0;
         
    }

}
