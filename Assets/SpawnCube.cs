using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour {
    public GameObject obj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnSphere()
    {
        Debug.Log("BEAT");
        //Instantiate(obj,this.transform.position,this.transform.rotation);
    }
}
