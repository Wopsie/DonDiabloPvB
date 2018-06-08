using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class LevelData : ScriptableObject{
    public GameObject levelObject;
    public float roadWidth;
    public Vector2[] points;
    public PropData[] propData;
    public float pointSpacing;
    public float textureTiling;
    public List<Transform> buildingsPositions;
    public List<Transform> backgroundObjsColl;
}

/// <summary>
/// class for storing the position and rotation of a prop
/// </summary>
public class PropData{
    public Vector3 position;
    public Quaternion rotation;

    public Matrix4x4 matrix{
        get { return Matrix4x4.TRS(position, rotation, Vector3.one); }
    }

    public PropData(Vector3 pos, Quaternion rot){
        this.position = pos;
        this.rotation = rot;
    }
}