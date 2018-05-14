using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;

public class PathPlacer : MonoBehaviour {

    public float spacing = 1f;
    public float resolution = 1;
    //public GameObject road;

    private void Start(){
        Vector2[] points = FindObjectOfType<PathCreator>().path.CalculateEvenSpacePoints(spacing, resolution);
        foreach (Vector2 p in points){
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //var g = (GameObject)Instantiate(road);
            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * 20f;
        }

        /*
        for (int i = 0; i < points.Length; i++){
            var g = (GameObject)Instantiate(road, transform, false);
            if(i >= points.Length){
                g.transform.position = points[i];
                g.transform.localScale = Vector3.one * spacing * 20f;
                g.transform.LookAt(points[0]);
            }else{
                g.transform.position = points[i];
                g.transform.localScale = Vector3.one * spacing * 20f;
                g.transform.LookAt(points[i+1]);
            }
            g.transform.localEulerAngles = new Vector3(g.transform.localEulerAngles.x, g.transform.localEulerAngles.y, 90);
        }
        */
    }
}
