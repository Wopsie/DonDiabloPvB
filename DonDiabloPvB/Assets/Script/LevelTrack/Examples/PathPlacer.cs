using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;

public class PathPlacer : MonoBehaviour {

    public float spacing = 1f;
    public float resolution = 1;
    public GameObject trackProp1;
    [Tooltip("The object that the player will use to navigate the track")]
    public GameObject playerTrackPoint;
    private GameObject[] trackedObjs;
    private Vector3 gScaleFix = new Vector3(1, -1, 1);

    public void PlacePath(Vector2[] points, Vector3[] dstToMeshEdgePerPoint, float meshWidth){

        DestroyTrackedObjects();

        if (!playerTrackPoint){
            Debug.LogWarning("The path placer has no player tracking point obj assigned");
            return;
        }else if( trackedObjs == null){
            trackedObjs = new GameObject[0];
        }

        //Vector2[] points = FindObjectOfType<PathCreator>().path.CalculateEvenSpacePoints(spacing, resolution);
        trackedObjs = new GameObject[points.Length];

        for (int i = 0; i < points.Length; i++){
            trackedObjs[i] = Instantiate(playerTrackPoint, transform);
            trackedObjs[i].tag = Tags.WaypointTag;
            trackedObjs[i].transform.position = points[i];
            trackedObjs[i].transform.localScale = Vector3.one * spacing * 0.5f;
            trackedObjs[i].GetComponent<PlayerTrackingPoint>().PointIndex = i;

            //rotate the prop objects to their center point
            //REBUILD WITH EXCLUSIVELY QUATERNIONS
            GameObject g = Instantiate(trackProp1, trackedObjs[i].transform.position + dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            Vector3 v = trackedObjs[i].transform.position - g.transform.position;
            Debug.DrawRay(g.transform.position, v, Color.red);
            g.transform.localRotation = Quaternion.LookRotation(v);
            g.transform.rotation *= Quaternion.Euler(90, 0, 90);

            //if(g.transform.localEulerAngles.y >= 89 && g.transform.localEulerAngles.y <= 91)
            //    g.transform.localScale = gScaleFix;

            g = Instantiate(trackProp1, trackedObjs[i].transform.position - dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            //REBUILD WITH EXCLUSIVELY QUATERNIONS
            v = trackedObjs[i].transform.position - g.transform.position;
            Debug.DrawRay(g.transform.position, v, Color.red);
            g.transform.localRotation= Quaternion.LookRotation(v);
            g.transform.localRotation *= Quaternion.Euler(90, 0, 90);

            //if(g.transform.localEulerAngles.y == 90)
            //    g.transform.localScale = gScaleFix;
        }
    }

    void DestroyTrackedObjects(){
        if (trackedObjs != null){
            for (int i = 0; i < trackedObjs.Length; i++){
                DestroyImmediate(trackedObjs[i]);
            }
        }
    }
}
