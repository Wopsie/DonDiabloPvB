﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;

public class PathPlacer : MonoBehaviour {

    public float spacing = 1f;
    public float resolution = 1;
    public GameObject playerTrackPoint;
    private GameObject[] trackedObjs;

    private void Start(){
        Vector2[] points = FindObjectOfType<PathCreator>().path.CalculateEvenSpacePoints(spacing, resolution);
        /*
        foreach (Vector2 p in points){
            //GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var g = (GameObject)Instantiate(playerTrackPoint);
            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * 0.5f;
        }
        */

        for (int i = 0; i < points.Length; i++){
            var g = (GameObject)Instantiate(playerTrackPoint, transform);
            g.transform.position = points[i];
            g.transform.localScale = Vector3.one * spacing * 0.5f;
            g.GetComponent<PlayerTrackingPoint>().PointIndex = i;
        }
    }

    public void PlacePath(){
        if (!playerTrackPoint){
            Debug.LogWarning("The path placer has no player tracking point obj assigned");
            return;
        }
        
        for (int i = 0; i < trackedObjs.Length; i++){
            if (trackedObjs[i] != null)
                DestroyImmediate(trackedObjs[i]);
        }

        Vector2[] points = FindObjectOfType<PathCreator>().path.CalculateEvenSpacePoints(spacing, resolution);
        trackedObjs = new GameObject[points.Length];

        for (int i = 0; i < points.Length; i++){
            var g = (GameObject)Instantiate(playerTrackPoint, transform);
            g.transform.position = points[i];
            g.transform.localScale = Vector3.one * spacing * 0.5f;
            g.GetComponent<PlayerTrackingPoint>().PointIndex = i;
            trackedObjs[i] = g;
        }
    }
}
