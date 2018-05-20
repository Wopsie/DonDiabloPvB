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
            GameObject g = Instantiate(trackProp1, trackedObjs[i].transform.position + dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            //REBUILD WITH QUATERNIONS
            //g.transform.LookAt(trackedObjs[i].transform.position);
            //g.transform.localEulerAngles = new Vector3(g.transform.localEulerAngles.x + 90, g.transform.localEulerAngles.y, g.transform.localEulerAngles.z + 90);

            Quaternion rot = Quaternion.LookRotation(trackedObjs[i].transform.position, g.transform.position);
            g.transform.rotation = rot;

            g = Instantiate(trackProp1, trackedObjs[i].transform.position - dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            //REBUILD WITH QUATERNIONS
            //g.transform.LookAt(trackedObjs[i].transform.position);
            //g.transform.localEulerAngles = new Vector3(g.transform.localEulerAngles.x - 90, g.transform.localEulerAngles.y, g.transform.localEulerAngles.z - 90);
            //rotate object over x axis to look at center point
            //rot = Quaternion.LookRotation(trackedObjs[i].transform.position, g.transform.position);
            //g.transform.rotation = rot;


            /*
            verts[vertIndex] = points[i] + left * roadWidth * 0.5f;
            verts[vertIndex + 1] = points[i] - left * roadWidth * 0.5f;
            */
        }
    }

    void DestroyTrackedObjects(){
        if (trackedObjs[0]){
            for (int i = 0; i < trackedObjs.Length; i++){
                DestroyImmediate(trackedObjs[i]);
            }
        }
    }
}
