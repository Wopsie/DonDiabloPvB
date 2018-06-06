using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour {

    private NewPlayerMovement movement;
    private GameObject player;
    private GameObject managers; 

    private void Start(){
        managers = GameObject.FindWithTag(Tags.ManagersTag);
        player = GameObject.FindWithTag(Tags.PlayerTag);
        managers.GetComponent<ObstacleHelper>().enabled = true;
        movement = player.GetComponent<NewPlayerMovement>();
        movement.enabled = true;
        movement.SetPlayerPosition(new Vector3(movement.waypoints[0].transform.position.x, 0.75f, movement.waypoints[0].transform.position.z));
        //enable the obstacle helper and player movement
        //set the position of the player on the right place
    }

    private void OnDestroy(){
        movement.enabled = false;
        managers.GetComponent<ObstacleHelper>().enabled = false;
    }

}
