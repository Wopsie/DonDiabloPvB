using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private PlayerInput input;
    private int LanesActive;
    private Dictionary<TrackSpawner, int> spawnerList = new Dictionary<TrackSpawner, int>();

    private void Start(){
        GameObject[]  spawners = GameObject.FindGameObjectsWithTag(Tags.SpawnerTag);
        for (int i = 0; i < spawners.Length; i++){

            if(spawners[i].gameObject.name == "TrackSpawner"){
                spawnerList.Add(spawners[i].GetComponent<TrackSpawner>(), 0);
            }else{
                spawnerList.Add(spawners[i].GetComponent<TrackSpawner>(), 1);
            }
        }

        input = GetComponent<PlayerInput>();
        input.OnPressButton += GetPress;
        input.OnHoldButton += GetHold;
        input.OnReleaseButton += GetRelease;
    }

    void GetPress(){
        Debug.Log("PRESS");
        //if no obstacle is in front
        //slerp to other lane

        if(transform.position.x == 2.6){
            MoveLane(true);
        }else{
            MoveLane(false);
        }
    }

    void GetHold(){
        Debug.Log("HOLD");
    }

    void GetRelease(){
        Debug.Log("RELEASE");
    }

    void MoveLane(bool left){
        if (left){
            
        }
    }
}
