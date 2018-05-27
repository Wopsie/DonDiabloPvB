using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;
using UnityEditor;

public class PathPlacer : MonoBehaviour {

    public float spacing = 1f;
    public float resolution = 1;
    public GameObject trackProp1;
    [Tooltip("The object that the player will use to navigate the track")]
    public GameObject playerTrackPoint;
    private GameObject[] trackedObjs;
    private Vector3 gScaleFix = new Vector3(1, -1, 1);

    public void PlacePath(Vector2[] points, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool placeProps, bool makeStatic, bool placePoints){

        DestroyTrackedObjects();

        if (!placePoints)
            return;

        if (!playerTrackPoint){
            Debug.LogWarning("The path placer has no player tracking point obj assigned");
            return;
        }else if( trackedObjs == null){
            trackedObjs = new GameObject[0];
        }

        trackedObjs = new GameObject[points.Length];

        for (int i = 0; i < points.Length; i++){
            trackedObjs[i] = Instantiate(playerTrackPoint, transform);
            trackedObjs[i].tag = Tags.WaypointTag;
            trackedObjs[i].transform.position = new Vector3(points[i].x, 0, points[i].y);
            trackedObjs[i].transform.localScale = Vector3.one * spacing * 0.5f;
            trackedObjs[i].GetComponent<PlayerTrackingPoint>().PointIndex = i;

            if (makeStatic)
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.BatchingStatic);

            if (placeProps){
                PlaceProp(true, trackedObjs, dstToMeshEdgePerPoint, meshWidth, makeStatic, i);
                PlaceProp(false, trackedObjs, dstToMeshEdgePerPoint, meshWidth, makeStatic, i);
            }
        }
    }

    /// <summary>
    /// method places props along road mesh
    /// </summary>
    /// <param name="left"></param>
    /// <param name="trackedObjs"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="meshWidth"></param>
    /// <param name="makeStatic"></param>
    /// <param name="i"></param>
    void PlaceProp(bool left, GameObject[] trackedObjs, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool makeStatic, int i){
        //place object at position depending on position of the trackedObj center point in the mesh
        GameObject g;
        if (left){
            g = Instantiate(trackProp1, trackedObjs[i].transform.position + new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y) /*dstToMeshEdgePerPoint[i]*/ * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
        }else{
            g = Instantiate(trackProp1, trackedObjs[i].transform.position - new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y) /*dstToMeshEdgePerPoint[i]*/ * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
        }
        //work out rotation direction that object should take 
        Vector3 v = trackedObjs[i].transform.position - g.transform.position;
        //Debug.DrawRay(g.transform.position, v, Color.red);
        g.transform.localRotation = Quaternion.LookRotation(v);
        g.transform.rotation *= Quaternion.Euler(0, -90, 0);

        if (makeStatic)
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.BatchingStatic);
    }

    void DestroyTrackedObjects(){
        if (trackedObjs != null){
            for (int i = 0; i < trackedObjs.Length; i++){
                DestroyImmediate(trackedObjs[i]);
            }
        }
    }
}
