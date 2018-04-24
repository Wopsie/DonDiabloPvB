using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour {

    [SerializeField] //track prefab & parent objects
    private GameObject trackModel, primeTrackParent, secondTrackParent;
    [SerializeField] //amount of track parts to be instantiated at start of game (x2)
    private int trackLength = 20;
    private Vector3 trackStart;
    private List<GameObject> primeTrackPartsList = new List<GameObject>(); //holds track parts for the seperate lanes
    private List<GameObject> secondTrackPartsList = new List<GameObject>();
    private bool secondLane = false;

    private void Start(){
        for (int i = 0; i < trackLength; i++){
            var track = Instantiate(trackModel, new Vector3(0,0,i * 1.3f), Quaternion.identity, primeTrackParent.transform);
            primeTrackPartsList.Add(track);

            //track = Instantiate(secondTrackParent, new Vector3(1.3f, 0, i * 1.3f), Quaternion.identity, secondTrackParent.transform);
            //secondTrackPartsList.Add(track);
        }
        trackStart = new Vector3(0, 0, trackLength * 1.3f);
    }

    private void Update(){
        for (int i = 0; i < trackLength; i++){
            //for primary lane
            if (primeTrackPartsList[i].transform.position.z <= -0.2f){
                ResetTrackPartAt(primeTrackPartsList, i);
            }
            //for secondary lane
            //if(secondTrackPartsList[i].transform.position.z <= -0.2f){
            //    ResetTrackPartAt(secondTrackPartsList, i);
            //}
        }
    }

    void ResetTrackPartAt(List<GameObject> list ,int index){
        list[index].transform.position += trackStart;
    }

    void RemoveTrackPartAt(int index){
        Destroy(primeTrackPartsList[index]);
        primeTrackPartsList.RemoveAt(index);
    }
}
