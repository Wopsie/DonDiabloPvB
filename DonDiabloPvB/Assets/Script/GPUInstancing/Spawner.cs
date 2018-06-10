using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private GPUInstancer instancing;
    public Transform[] objs;
    public int instances = 100;
    private Vector3 maxPos = new Vector3(10, 10, 10);

    private void Start(){
        instancing = FindObjectOfType<GPUInstancer>();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            for (int i = 0; i < instances; i++){
                //GPUInstancing.Instance.AddObj(objs[0]);
            }
        }else if (Input.GetKeyDown(KeyCode.Mouse1)){
            for (int i = 0; i < instances; i++){
                //instancing.AddObject(objs[1], new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z)));
            }
        }else if (Input.GetKeyDown(KeyCode.Mouse2)){
            for (int i = 0; i < instances; i++){
                //instancing.AddObject(objs[2], new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z)));
            }
        }
    }
}
