#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineEditor;
using UnityEditor;

public class PathPlacer : MonoBehaviour{
    [Tooltip("Frequency of building spawning. More is less")]
    [Range(2, 20)]
    public int buildingFrequency = 5;
    [Range(10, 50)]
    private int TunnelLength = 15;
    public float buildingDistance = 50;
    [Tooltip("First enter the clamp, then enter the holder, then enter the arrow")]
    public GameObject[] trackProps;
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
    [HideInInspector]//the gameobject that holds all of the visual, non-interactable elements on the track
    public GameObject backgroundHolder;

    /// <summary>
    /// Generates road properties such as waypoints and visual elements along a given array of points
    /// </summary>
    /// <param name="points"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="meshWidth"></param>
    /// <param name="placeProps"></param>
    /// <param name="placePoints"></param>
    /// <param name="placeBuildings"></param>
    /// <param name="finalize"></param>
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

        backgroundHolder = new GameObject();
        backgroundHolder.transform.name = "MeshHolder";
        backgroundHolder.tag = Tags.MeshHolderTag;
        trackedObjs = new GameObject[points.Length];
        completeTunnel = new GameObject[TunnelLength + 1];

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
                }
            }

            //place buildings set distance from track with certain margin
            if (placeBuildings){
                if (buildingClusters == null){
                    Debug.LogError("No building cluster prefabs selected");
                    return;
                }
                //place the buildings every so many points
                if (i % (buildingClusters.Length * buildingFrequency) == 0){
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
    }

    /// <summary>
    /// Place buildings next to the road and as child of MeshHolder gameobject 
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="trackedObj"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="finalizeBuildings"></param>
    /// <param name="index"></param>
    void PlaceBuildings(GameObject trackedObj, Vector3[] dstToMeshEdgePerPoint, bool finalizeBuildings, int index){
        int i = Random.Range(0, buildingClusters.Length);
        //only render a preview of the buildings if the level is not being finalized
        GameObject g;
        //place preview buildings on the currently received gameobject trackedObj
        for (int j = 0; j < 2; j++){
            if (j == 1)
                g = Instantiate(buildingClusters[i], (trackedObj.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, -0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);
            else
                g = Instantiate(buildingClusters[i], (trackedObj.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0.25f, dstToMeshEdgePerPoint[index].y) * buildingDistance), Quaternion.identity, trackedObj.transform);
            //set parent transform of building to the meshholder 
            g.transform.SetParent(backgroundHolder.transform);
            //Randomize rotation
            int dir = Random.Range(0, 360);
            g.transform.rotation = Quaternion.Euler(0, dir, 0);
        }
    }

    /// <summary>
    /// Place road props along the road and as children of MeshHolder gameobject
    /// </summary>
    /// <param name="left"></param>
    /// <param name="trackedObj"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="meshWidth"></param>
    /// <param name="finalizeProps"></param>
    /// <param name="index"></param>
    void PlaceRoadProps(GameObject trackedObj, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool finalizeProps, int index){
        //place object at position depending on position of the trackedObj center point in the mesh
        GameObject g;
        for (int j = 0; j < trackProps.Length; j++)
        {
            for (int i = 0; i < 2; i++){
                if (i == 1)//left
                    g = Instantiate(trackProps[j], trackedObj.transform.position + new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f, Quaternion.identity, trackedObj.transform);
                else//right
                    g = Instantiate(trackProps[j], trackedObj.transform.position - new Vector3(dstToMeshEdgePerPoint[index].x, 0, dstToMeshEdgePerPoint[index].y) * meshWidth * 0.5f, Quaternion.identity, trackedObj.transform);

                g.transform.SetParent(backgroundHolder.transform);
                //work out rotation direction that object should take 
                Vector3 v = trackedObj.transform.position - g.transform.position;
                g.transform.localRotation = Quaternion.LookRotation(v);
                g.transform.rotation *= Quaternion.Euler(0, 180, 0);

                //set positions specific for each prop
                if (j == 1) {
                    //this is the holder prop
                    if (index % 3 == 0){ //this statement is to make the prop not spawn on every single tracking point
                        if(i == 1){//left
                            g.transform.localPosition -= Vector3.forward * 0.5f;
                            g.transform.localPosition -= Vector3.left * 1f;
                        }
                        else{//right
                            g.transform.localPosition -= Vector3.forward * 0.5f;
                            g.transform.localPosition += Vector3.left * 0.5f;
                        }
                    }else{
                        DestroyImmediate(g);
                    }
                } else if (j == 2) {
                    //this is the arrow prop
                    if(index % 5 == 0){
                        g.transform.position += (Vector3.up * 0.5f);
                        if (i == 1){//left
                            g.transform.position -= Vector3.forward;
                        }else{//right
                            g.transform.position += Vector3.forward;
                            g.transform.localScale = new Vector3(-g.transform.localScale.x, g.transform.localScale.y, g.transform.localScale.z);
                        }
                    }else{
                        DestroyImmediate(g);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Place tunnel at given length at start of road
    /// </summary>
    /// <param name="Pointvec"></param>
    /// <param name="i"></param>
    /// <param name="dstToMeshEdgePerPoint"></param>
    /// <param name="meshWidth"></param>
    /// <param name="doorPiece"></param>
    void PlaceTunnel(Vector2[] Pointvec, int i, Vector3[] dstToMeshEdgePerPoint, float meshWidth, bool doorPiece){
        GameObject g = tunnelGo;
        if (doorPiece){
            g = TunnelDoor;
        }

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
                DestroyImmediate(completeTunnel[i]);
            }
        }
        if(backgroundHolder != null){
            DestroyImmediate(backgroundHolder);
        }
    }

    /// <summary>
    /// remove all playerpoints present in the scene, tracked or not
    /// </summary>
    public void CleanScene(){
        DestroyTrackedObjects();
        GameObject[] g = GameObject.FindGameObjectsWithTag(Tags.WaypointTag);
        for (int i = 0; i < g.Length; i++){
            DestroyImmediate(g[i]);
        }

        g = GameObject.FindGameObjectsWithTag(Tags.MeshHolderTag);
        for (int i = 0; i < g.Length; i++){
            DestroyImmediate(g[i]);
        }
    }
}
#endif