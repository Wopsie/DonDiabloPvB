using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingPoint : MonoBehaviour {

    //set this index at creation
    public int pointIndex;
    public int PointIndex{
        get { return pointIndex; }
        set { pointIndex = value;}
    }
}
