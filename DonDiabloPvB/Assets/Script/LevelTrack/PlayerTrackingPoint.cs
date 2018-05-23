using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingPoint : MonoBehaviour {
    //set this index at creation
    //[HideInInspector]
    [SerializeField]
    private int pointIndex;
    public int PointIndex {
        get { return pointIndex;  }
        set { pointIndex = value; }
    }
    /*
    [HideInInspector]
    [SerializeField]
    private Vector3 localPosRelativeToWorld;
    public Vector3 LocalPosRelativeToWorld {
        get { return localPosRelativeToWorld; }
        set { localPosRelativeToWorld = value; }
    }
    
    [HideInInspector]
    [SerializeField]
    private Vector3[] meshEdgePosArray = new Vector3[2];
    public Vector3 this[int i]{
        get { return (i > meshEdgePosArray.Length) ? meshEdgePosArray[meshEdgePosArray.Length] : meshEdgePosArray[i]; }
        set { meshEdgePosArray[i] = value; }
    }
    */

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
    }
}
