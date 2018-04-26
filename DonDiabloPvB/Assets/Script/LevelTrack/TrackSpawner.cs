using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour {

    [SerializeField] //track prefab & parent objects
    private GameObject trackModel, primeTrackParent;
    [SerializeField] //amount of track parts to be instantiated at start of game (x2)
    private int trackLength = 20;
    [SerializeField]
    public float xPos;
    //[HideInInspector]
    public bool laneExists = true;
    private Vector3 trackStart;
    private List<GameObject> trackPartsList = new List<GameObject>(); //holds track parts for the seperate lanes


    private void Start(){
        for (int i = 0; i < trackLength; i++){
            var track = Instantiate(trackModel, new Vector3(xPos,0,i * 3.7f), Quaternion.identity, primeTrackParent.transform);
            trackPartsList.Add(track);
            track.SetActive(laneExists);
        }
        trackStart = new Vector3(0, 0, trackLength * 1.3f);
    }

    private void Update(){
        for (int i = 0; i < trackLength; i++){
            //for primary lane
            if (trackPartsList[i].transform.position.z <= -2f){
                ResetTrackPartAt(trackPartsList, i);
                trackPartsList[i].SetActive(laneExists);
            }
        }
    }

    void ResetTrackPartAt(List<GameObject> list ,int index){
        list[index].transform.position += trackStart;
    }

    void RemoveTrackPartAt(int index){
        Destroy(trackPartsList[index]);
        trackPartsList.RemoveAt(index);
    }
}
