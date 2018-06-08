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

        //add all of the buildings and props to the GPUInstancer
        for (int i = 0; i < LevelManager.Instance.level.buildingsPositions.Count; i++){
            //for every building position that you have, randomly select a building to place there
            //LevelManager.Instance.level.backgroundObjsColl[Random.Range(0, 3)]
            //int rand = Random.Range(0, 3);
            //Instantiate(LevelManager.Instance.level.backgroundObjsColl[rand].gameObject, LevelManager.Instance.level.backgroundObjsColl[rand].position, Quaternion.identity);
            //Debug.Log(LevelManager.Instance.level.backgroundObjsColl[rand].position);
            //GPUInstancing.Instance.AddObjTrans(LevelManager.Instance.level.backgroundObjsColl[rand], this.transform);
            //GPUInstancing.Instance.AddObjTrans(LevelManager.Instance.level.backgroundObjsColl[Random.Range(0, 3)], LevelManager.Instance.level.buildingsPositions[i], this.transform);
        }
        
        /*
        //add props to GPUInstancer
        //PROPDATA IS NULL, FIGURE OUT WHY
        for (int i = 0; i < LevelManager.Instance.level.propData.Length; i++){
            GPUInstancing.Instance.AddObj(LevelManager.Instance.level.backgroundObjsColl[LevelManager.Instance.level.backgroundObjsColl.Count], LevelManager.Instance.level.propData[i].position, Vector3.one, LevelManager.Instance.level.propData[i].rotation, this.transform, false);
        }
        */
    }

    private void OnDestroy(){
        movement.enabled = false;
        managers.GetComponent<ObstacleHelper>().enabled = false;
    }

}
