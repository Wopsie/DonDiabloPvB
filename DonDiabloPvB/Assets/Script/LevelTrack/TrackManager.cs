using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    private GameObject[] spawnerObjs;
    private TrackSpawner[] spawners;

    private void Start(){
        spawnerObjs = GameObject.FindGameObjectsWithTag(Tags.SpawnerTag);

        for (int i = 0; i < spawnerObjs.Length; i++){
            spawners[i] = spawnerObjs[i].GetComponent<TrackSpawner>();
        }
    }

}
