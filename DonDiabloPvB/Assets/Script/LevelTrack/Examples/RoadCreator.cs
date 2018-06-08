#if (UNITY_EDITOR) 
using SplineEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(PathPlacer))]
public class RoadCreator : MonoBehaviour{
    [Range(0.5f, 2.5f)]
    public float spacing = 1;
    public float roadWidth = 1;
    [Tooltip("Live road creation updates")]
    public bool autoUpdate;
    [Tooltip("Live waypoint placing updates")]
    public bool placePoints;
    [Tooltip("Live prop placing updates directly adjacent to road edge")]
    public bool placeProps;
    public float tiling = 1;
    [HideInInspector]
    public Vector2[] points;
    [HideInInspector]
    public List<Transform> buildingPositions;
    [HideInInspector]
    public PropData[] propDataArray;
    [HideInInspector]
    public GameObject[] backgroundObjs;
    [HideInInspector]
    public GameObject prop;

    public MeshFilter filter;
    public new MeshRenderer renderer;

    private Vector3[] vertexOffsetVectors;
    private MeshGenerator generator;


    public void UpdateRoad(){
        if (!renderer || !filter){
            Debug.LogWarning("No target mesh components selected; Automatic detection");
            renderer = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshRenderer>();
            filter = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshFilter>();
            return;
        }

        if (!generator){
            generator = GameObject.FindObjectOfType<MeshGenerator>();
        }

        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        //filter.mesh = CreateRoadMesh(points, path.IsClosed);
        filter.mesh = GetRoad();
        vertexOffsetVectors = generator.vertexOffsetVectors;
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
        //method to place waypoints & props along track
        GetComponent<PathPlacer>().GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, placeProps, placePoints, false, false);
    }

    public void PlaceBuildings(){
        autoUpdate = false;
        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        vertexOffsetVectors = generator.vertexOffsetVectors;
        GetComponent<PathPlacer>().GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, placeProps, placePoints, true, false);
    }

    public void FinalizePath(){
        if (!renderer || !filter){
            Debug.LogError("No target mesh components selected; Automatic detection");
            renderer = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshRenderer>();
            filter = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshFilter>();
        }

        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        filter.mesh = GetRoad();
        vertexOffsetVectors = generator.vertexOffsetVectors;
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
        PathPlacer placer = GetComponent<PathPlacer>();
        placer.CleanScene();
        placer.GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, true, true, true, true);
        buildingPositions = placer.buildingPosList;

        Debug.Log(placer.propPosRotData + " " + placer.propPosRotData.Length);
        propDataArray = placer.propPosRotData;

        //fill the backgroundobject prefabs array with the buildings and prop
        backgroundObjs = new GameObject[placer.buildingClusters.Length + 1];
        for (int i = 0; i < backgroundObjs.Length; i++){
            if (i < backgroundObjs.Length - 1)
                backgroundObjs[i] = placer.buildingClusters[i];
            else //insert prop object last into the array
                backgroundObjs[i] = placer.trackProp1;
        }
    }

    public void CleanScene(){
        GetComponent<PathPlacer>().CleanScene();
    }

    Mesh GetRoad(){
        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        return generator.CreateRoadMesh(points, false, roadWidth);
    }
}
#endif