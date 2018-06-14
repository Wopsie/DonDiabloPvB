#if (UNITY_EDITOR) 
using SplineEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Class that handles the road/path, places building around track.
/// </summary>
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
    public GameObject[] buildingModelsArr;

    public MeshFilter filter;
    public new MeshRenderer renderer;

    private Vector3[] vertexOffsetVectors;
    private MeshGenerator generator;
    [HideInInspector]
    public GameObject backgroundElementsGo;

    /// <summary>
    /// Get and set all data need to place the road in parts.
    /// </summary>
    public void UpdateRoad(){
        //clean this up
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

        filter.mesh = GetRoad();
        vertexOffsetVectors = generator.vertexOffsetVectors;
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
        //method to place waypoints & props along track
        PathPlacer placer = GetComponent<PathPlacer>();
        placer.GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, placeProps, placePoints, false, false);
    }
    /// <summary>
    /// Checks space around road is free to place building around the track.
    /// </summary>
    public void PlaceBuildings(){
        autoUpdate = false;
        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        vertexOffsetVectors = generator.vertexOffsetVectors;
        GetComponent<PathPlacer>().GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, placeProps, placePoints, true, false);
    }

    /// <summary>
    /// FinalizePath after checks.
    /// </summary>
    public void FinalizePath(){
        if (!renderer || !filter){
            Debug.LogError("No target mesh components selected; Automatic detection");
            renderer = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshRenderer>();
            filter = GameObject.FindWithTag(Tags.targetRoadMeshTag).GetComponent<MeshFilter>();
        }
        autoUpdate = false;
        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenSpacePoints(spacing);
        filter.mesh = GetRoad();
        vertexOffsetVectors = generator.vertexOffsetVectors;
        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * 0.05f);
        renderer.sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
        PathPlacer placer = GetComponent<PathPlacer>();
        placer.CleanScene();
        placer.GenerateRoadProperties(points, vertexOffsetVectors, roadWidth, true, true, true, true);
        backgroundElementsGo = placer.backgroundHolder;
        //Do the same for props later


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