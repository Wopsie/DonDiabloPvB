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

    public void PlacePath(Vector2[] points, Vector2[] dstToMeshEdgePerPoint, float meshWidth){

        if (!playerTrackPoint){
            Debug.LogWarning("The path placer has no player tracking point obj assigned");
            return;
        }else if( trackedObjs == null){
            trackedObjs = new GameObject[0];
        }
        
        for (int i = 0; i < trackedObjs.Length; i++){
            if (trackedObjs[i] != null)
            {
                DestroyImmediate(trackedObjs[i]);

            }
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

            GameObject g = Instantiate(trackProp1, points[i] + dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            //g.transform.localEulerAngles = new Vector3(0, points[i].x - transform.localPosition.x, points[i].y - transform.localPosition.y);
            //g.transform.localEulerAngles = new Vector3(-90, 0, 0);
            g.transform.LookAt(new Vector3(-90, points[i].y, 0));
            //calculate the edge
            //rotate object to the points[i] direction. Target pos - own pos
            g = Instantiate(trackProp1, points[i] - dstToMeshEdgePerPoint[i] * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
            //g.transform.localEulerAngles = new Vector3(-90, 0, 0);
            g.transform.LookAt(new Vector3(-90, points[i].y, 0));

            /*
            verts[vertIndex] = points[i] + left * roadWidth * 0.5f;
            verts[vertIndex + 1] = points[i] - left * roadWidth * 0.5f;
            */
        }
    }
}
