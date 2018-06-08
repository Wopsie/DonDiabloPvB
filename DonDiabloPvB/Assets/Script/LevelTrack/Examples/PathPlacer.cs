#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;
using UnityEditor;

public class PathPlacer : MonoBehaviour{
    //public float spacing = 1f;
    //public int propSpacing = 1;
    [Tooltip("Frequency of building spawning. More is less")]
    [Range(2, 20)]
    public int buildingFrequency = 5;
    [Range(10, 50)]
    private int TunnelLength = 15;
    public float buildingDistance = 50;
    public GameObject trackProp1;
    [HideInInspector]
    public List<Transform> buildingPosList;
    [HideInInspector]
    public PropData[] propPosRotData;
    [SerializeField]
    private GameObject tunnelGo;
    [SerializeField]
    private GameObject TunnelDoor;
    [SerializeField]
    private GameObject[] completeTunnel;
    [Tooltip("The object that the player will use to navigate the track")]
    public GameObject playerTrackPoint;
    public GameObject[] buildingClusters;
    private GameObject[] trackedObjs;

    public void GenerateRoadProperties(Vector2[] points, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool placeProps, bool placePoints, bool placeBuildings, bool finalize){
        DestroyTrackedObjects();

        //behaviour for allowing points to be placed when other things are being placed that depend on them
        if (placeProps && !placePoints)
            Debug.LogWarning("Props cannot be placed if points are not placed");

        if (!placePoints)
            return;

        if (!playerTrackPoint){
            Debug.LogWarning("The path placer has no player tracking point obj assigned");
            return;
        }else if (trackedObjs == null){
            trackedObjs = new GameObject[0];
        }

        trackedObjs = new GameObject[points.Length];
        completeTunnel = new GameObject[TunnelLength + 1];
        if (finalize){//only set these arrays if path is being finalized, as else they are unnecessary
            propPosRotData = new PropData[dstToMeshEdgePerPoint.Length * 2];
            //buildings array need only be as long as the amount of building positions that are created

            //convert buildingPos array to list
            //add the building position to it when it is determined
            buildingPosList = new List<Transform>();
            //buildingPosColl = new Vector3[dstToMeshEdgePerPoint.Length * 2];
        }

        for (int i = 0; i < points.Length; i++){
            trackedObjs[i] = Instantiate(playerTrackPoint, transform);
            trackedObjs[i].tag = Tags.WaypointTag;
            trackedObjs[i].transform.position = new Vector3(points[i].x, 0, points[i].y);
            trackedObjs[i].transform.localScale = Vector3.one * 0.5f;
            trackedObjs[i].GetComponent<PlayerTrackingPoint>().PointIndex = i;

            if (finalize){
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.BatchingStatic);
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.OccludeeStatic);
                GameObjectUtility.SetStaticEditorFlags(trackedObjs[i], StaticEditorFlags.OccluderStatic);
            }

            if (i <= (TunnelLength)){
                //PlaceTunnel(points,i, dstToMeshEdgePerPoint, meshWidth, trackedObjs[i]);
            }else{
                //place props along track edges
                if (placeProps){
                    PlaceRoadProps(trackedObjs[i], dstToMeshEdgePerPoint, meshWidth, finalize, i);
                    PlaceRoadProps(trackedObjs[i], dstToMeshEdgePerPoint, meshWidth, finalize, i);
                }
            }

            //place buildings set distance from track with certain margin
            if (placeBuildings){
                if (buildingClusters == null){
                    Debug.LogError("No building cluster prefabs selected");
                    return;
                }

                if (i % (buildingClusters.Length * buildingFrequency) == 0){
                    PlaceBuildings(trackedObjs[i], dstToMeshEdgePerPoint, finalize, i);
                    PlaceBuildings(trackedObjs[i], dstToMeshEdgePerPoint, finalize, i);
                }
            }
        }

        //spawn the start tunnel
        for (int i = 0; i <= TunnelLength - 1; i++){
            PlaceTunnel(points, i, dstToMeshEdgePerPoint, meshWidth, false);
            if (i == TunnelLength - 1){
                // use this function to spawn the door
                /*
                PlaceTunnel(points, i, dstToMeshEdgePerPoint, meshWidth, false);
                print("spawn the door");
                */
            }
        }

        //new Vector3(points[i].x, 0, points[i].y);
    }

    /// <summary>
    /// place buildings preview or store the positions at which the buildings should be placed at runtime
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="trackedObj"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="finalizeBuildings"></param>
    /// <param name="index"></param>
    void PlaceBuildings(GameObject trackedObj, Vector3[] dstToMeshEdgePerPoint, bool finalizeBuildings, int index){
        int i = Random.Range(0, buildingClusters.Length);
        //only render a preview of the buildings if the level is not being finalized
        if (!finalizeBuildings){
            GameObject g;

            //place preview buildings on the currently received gameobject trackedObj
            for (int j = 0; j < 2; j++){
                if (j == 1)
                    g = Instantiate(buildingClusters[i], (trackedObj.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);
                else
                    g = Instantiate(buildingClusters[i], (trackedObj.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);

                //multiply scale * 100
                //g.transform.localScale *= 100;
                //Set random rotation
                int dir = Random.Range(0, 360);
                g.transform.rotation = Quaternion.Euler(0, dir, 0);
            }
        }else{
            //Store the positions at which the buildings must be placed at runtime
            //buildingPosList.Add((trackedObj.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance));
            //buildingPosList.Add((trackedObj.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance));

            //instantiate temporary gameobjects for the buildings to store the values because idk how to deal with transforms & i dont have internet
            for (int j = 0; j < 2; j++){
                GameObject g;
                if (j == 1)
                    g = Instantiate(buildingClusters[i], (trackedObj.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);
                else
                    g = Instantiate(buildingClusters[i], (trackedObj.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);

                //Set random rotation
                int dir = Random.Range(0, 360);
                g.transform.rotation = Quaternion.Euler(0, dir, 0);

                buildingPosList.Add(g.transform);
                //DestroyImmediate(g);
            }

            //buildingPosColl[index] = (trackedObjs[index].transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance);
            //buildingPosColl[index+1] = (trackedObjs[index].transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance);

            //unsure whenever to use GPU Instancing to render buildings or just Objectpool
        }
    }

    /// <summary>
    /// method places props along road mesh
    /// </summary>
    /// <param name="left"></param>
    /// <param name="trackedObjs"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="meshWidth"></param>
    /// <param name="finalizeProps"></param>
    /// <param name="index"></param>
    void PlaceRoadProps(GameObject trackedObjs, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool finalizeProps, int index){
        //place object at position depending on position of the trackedObj center point in the mesh
        if (!finalizeProps){
            GameObject g;

            for (int i = 0; i < 2; i++){
                if (i == 1)
                    g = Instantiate(trackProp1, trackedObjs.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f, Quaternion.identity, trackedObjs.transform);
                else
                    g = Instantiate(trackProp1, trackedObjs.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f, Quaternion.identity, trackedObjs.transform);

                //work out rotation direction that object should take 
                Vector3 v = trackedObjs.transform.position - g.transform.position;
                g.transform.localRotation = Quaternion.LookRotation(v);
                g.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
        }else{
            //store the positions for the props on both sides of the track
            for (int i = 0; i < 2; i++){
                Vector3 pos;
                Vector3 v;
                Quaternion rot;

                if (i == 1){
                    pos = trackedObjs.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f;
                    v = trackedObjs.transform.position - pos;
                    rot = Quaternion.LookRotation(v);
                    rot *= Quaternion.Euler(0, -90, 0);
                    PropData prop = new PropData(pos, rot);
                    propPosRotData[index] = prop;
                }else{
                    pos = trackedObjs.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f;
                    v = trackedObjs.transform.position - pos;
                    rot = Quaternion.LookRotation(v);
                    rot *= Quaternion.Euler(0, -90, 0);
                    PropData prop = new PropData(pos, rot);
                    propPosRotData[index + 1] = prop;
                }
            }
        }
    }

    void PlaceTunnel(Vector2[] Pointvec, int i, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool doorPiece){
        GameObject g = tunnelGo;
        if (doorPiece){
            g = TunnelDoor;
        }

        print(g.name);
        Vector3 shinVec = new Vector3(Pointvec[i].x, 0, Pointvec[i].y);

        completeTunnel[i] = Instantiate(g, shinVec + new Vector3(dstToMeshEdgePerPoint[i].x, 0, dstToMeshEdgePerPoint[i].y) * meshWidth * 0f, Quaternion.identity, trackedObjs[i].transform);
        Vector3 neoVec = new Vector3(Pointvec[i + 1].x, 0, Pointvec[i + 1].y);
        Vector3 v = neoVec - completeTunnel[i].transform.position;

        completeTunnel[i].transform.localRotation = Quaternion.LookRotation(v);
        if (!doorPiece){
            completeTunnel[i].transform.Rotate(new Vector3(0, 90, 0));
            completeTunnel[i].transform.localScale = new Vector3(13.2f, 24f, 24f);
        }
    }

    /// <summary>
    /// destroy the tracked playerpoints & tunnel pieces in the scene
    /// </summary>
    void DestroyTrackedObjects(){
        if (trackedObjs != null){
            for (int i = 0; i < trackedObjs.Length; i++){
                DestroyImmediate(trackedObjs[i]);
            }
        }
        if (completeTunnel != null){
            for (int i = 0; i < completeTunnel.Length; i++){
                print("do you destroy");
                DestroyImmediate(completeTunnel[i]);
            }
        }
    }

    /// <summary>
    /// remove all playerpoints present in the scene, tracked or not
    /// </summary>
    public void CleanScene(){
        GameObject[] g = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        for (int i = 0; i < g.Length; i++){
            DestroyImmediate(g[i]);
        }
    }
}
#endif