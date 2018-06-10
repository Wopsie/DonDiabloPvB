using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Looks for all managers and relevant components in scene to start gameplay
/// </summary>
public class LevelStarter : MonoBehaviour{
    private NewPlayerMovement movement;
    private GameObject player;
    private GameObject managers;
    private GameObject backgroundHolder;

    private void Start(){
        managers = GameObject.FindWithTag(Tags.ManagersTag);
        player = GameObject.FindWithTag(Tags.PlayerTag);
        managers.GetComponent<ObstacleHelper>().enabled = true;
        movement = player.GetComponent<NewPlayerMovement>();
        movement.enabled = true;
        movement.SetPlayerPosition(new Vector3(movement.waypoints[0].transform.position.x, 0.75f, movement.waypoints[0].transform.position.z));

        //create the parent object for the background elements
        backgroundHolder = new GameObject();
        backgroundHolder.name = "MeshHolder";
        backgroundHolder.tag = Tags.MeshHolderTag;

        //loop through all of the children
        foreach (Transform child in LevelManager.Instance.level.levelBackgroundObj.transform){
            GPUInstancing.Instance.AddObjTrans(child, backgroundHolder.transform);
        }
    }

    private void OnDestroy(){
        movement.enabled = false;
        managers.GetComponent<ObstacleHelper>().enabled = false;
        Destroy(backgroundHolder);
    }
}