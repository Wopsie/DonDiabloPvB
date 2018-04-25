using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    private List<TrackSpawner> spawners = new List<TrackSpawner>();
    private List<bool> lanesActive = new List<bool>();
    private bool timerStarted = false;
    private System.Random rnd;

    private void Start(){
        GameObject[] spawnerObjs = GameObject.FindGameObjectsWithTag(Tags.SpawnerTag);
        for (int i = 0; i < spawnerObjs.Length; i++){
            spawners.Add(spawnerObjs[i].GetComponent<TrackSpawner>());
        }
    }

    private void Update()
    {
        //there should be at least one lane active

        //if there is one lane has been active for a set range time
        //activate the other lane
        //if 2 lanes have been active for a set range time
        //deactivate one

        //only 2 lanes can be active at a time
        
        //if timer is not running there is no possibility of changes happening
        //stop checking for lanes if timer is running
        if (!timerStarted)
        {
            for (int i = 0; i < spawners.Count; i++){
                if (spawners[i].laneExists){
                    lanesActive.Add(true);
                }
            }

            //ensure no more than 2 lanes can be active

            if(lanesActive.Count == 2){
                //never add another
                Debug.Log("REMOVE A LANE");
                StartCoroutine(LaneTimerRoutine(false));
            }

            if (lanesActive.Count == 1){
                //can add another
                Debug.Log("ADD NEW LANE");
                StartCoroutine(LaneTimerRoutine(true));
            }
        }

        lanesActive.Clear();
    }

    IEnumerator LaneTimerRoutine(bool addNew){
        timerStarted = true;
        yield return new WaitForSeconds(Random.Range(5, 30));
        if(addNew){
            //turn on the other lane
            for (int i = 0; i < spawners.Count; i++){
                if (!spawners[i].laneExists)
                    spawners[i].laneExists = true;
            }
        }else{
            //pick one of two random lanes to turn off
            int r = Random.Range(0, 2);
            spawners[r].laneExists = false;
            Debug.Log("turn off lane " + r);
        }

        timerStarted = false;
    }
}
