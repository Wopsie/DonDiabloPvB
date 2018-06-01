#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;
using UnityEditor;

public class PathPlacer : MonoBehaviour {
    public float spacing = 1f;
    public int propSpacing = 1;
    [Tooltip("Frequency of building spawning. More is less")][Range(2, 20)]
    public int buildingFrequency = 5;
    [Range(10, 50)]
    public float buildingDistance = 50;
    public GameObject trackProp1;
    [SerializeField]
    private GameObject tunnelGo;
    [Tooltip("The object that the player will use to navigate the track")]
    public GameObject playerTrackPoint;
    public GameObject[] buildingClusters;
    private GameObject[] trackedObjs;

    public void GenerateRoadProperties(Vector2[] points, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool placeProps, bool placePoints, bool placeBuildings, bool makeStatic){
        DestroyTrackedObjects();

        //behaviour for allowing points to be placed when other things are being placed that depend on them
        if (placeProps && !placePoints)
            Debug.LogWarning("Props cannot be placed if points are not placed");

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

            if (makeStatic){
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.BatchingStatic);
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.OccludeeStatic);
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.OccluderStatic);
            }

            if (i <= 5)
            {
                PlaceTunnel(points,i, dstToMeshEdgePerPoint, meshWidth);

            }
            else
            {
                //place props along track edges
                if (placeProps)
                {
                    PlaceRoadProps(true, trackedObjs, dstToMeshEdgePerPoint, meshWidth, makeStatic, i);
                    PlaceRoadProps(false, trackedObjs, dstToMeshEdgePerPoint, meshWidth, makeStatic, i);
                }
            }
          

            //place buildings set distance from track with certain margin
            if (placeBuildings){

                if(buildingClusters == null){
                    Debug.LogError("No building cluster prefabs selected");
                    return;
                }

                if (i % (buildingClusters.Length * buildingFrequency) == 0){
                    PlaceBuildings(true, 1, trackedObjs, dstToMeshEdgePerPoint, makeStatic, i);
                    PlaceBuildings(false, 1, trackedObjs, dstToMeshEdgePerPoint, makeStatic, i);
                }
            }
        }
        // placeTunnelParts(trackedObjs, 5);
        //new Vector3(points[i].x, 0, points[i].y);
    }

    void PlaceBuildings(bool left, int layer, GameObject[] trackedObjs, Vector3[] dstToMeshEdgePerPoint, bool makeStatic, int index){
        int i = Random.Range(0, buildingClusters.Length);
        GameObject g;
        if (left){
            Debug.Log(buildingClusters[i]);
            g = Instantiate(buildingClusters[i], (trackedObjs[index].transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObjs[index].transform);
        }else{
            Debug.Log(buildingClusters[i]);
            g = Instantiate(buildingClusters[i], (trackedObjs[index].transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObjs[index].transform);
        }

        g.transform.localScale = (g.transform.localScale * 100);

        //Set random rotation
        int dir = Random.Range(0, 360);
        g.transform.rotation = Quaternion.Euler(0, dir, 0);

        if (makeStatic){
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.BatchingStatic);
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.OccludeeStatic);
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.OccluderStatic);
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
    void PlaceRoadProps(bool left, GameObject[] trackedObjs, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool makeStatic, int i){
        //place object at position depending on position of the trackedObj center point in the mesh
        GameObject g;
        if (left){
            g = Instantiate(trackProp1, trackedObjs[i].transform.position + new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y) * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
        }else{
            g = Instantiate(trackProp1, trackedObjs[i].transform.position - new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y) * meshWidth * 0.5f, Quaternion.identity, trackedObjs[i].transform);
        }
        //work out rotation direction that object should take 
        Vector3 v = trackedObjs[i].transform.position - g.transform.position;
        //Debug.DrawRay(g.transform.position, v, Color.red);
        g.transform.localRotation = Quaternion.LookRotation(v);
        g.transform.rotation *= Quaternion.Euler(0, -90, 0);

        if (makeStatic){
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.BatchingStatic);
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.OccludeeStatic);
            GameObjectUtility.SetStaticEditorFlags(g, StaticEditorFlags.OccluderStatic);
        }
    }

    void PlaceTunnel(Vector2[] trackedObjs, int i, Vector3[] dstToMeshEdgePerPoint, float meshWidth)
    {
        GameObject g = tunnelGo;

        Vector3 shinVec = new Vector3(trackedObjs[i].x, 0, trackedObjs[i].y);
        g = Instantiate(tunnelGo, shinVec + new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y)  * meshWidth * 0f, Quaternion.identity);
        Vector3 neoVec = new Vector3(trackedObjs[i + 1].x, 0, trackedObjs[i + 1].y);
        Vector3 v = neoVec - g.transform.position;
        g.transform.localRotation = Quaternion.LookRotation(v);
       // g.transform.LookAt(trackedObjs[i + 1].transform);


    }
        
    void DestroyTrackedObjects(){
        if (trackedObjs != null){
            for (int i = 0; i < trackedObjs.Length; i++){
                DestroyImmediate(trackedObjs[i]);
            }
        }
    }
}
#endif