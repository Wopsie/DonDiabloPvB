﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SplineEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(RoadCreator))]
public class RoadEditor : Editor{
    RoadCreator creator;

    private void OnSceneGUI(){
        if (creator.autoUpdate && Event.current.type == EventType.Repaint){
            creator.UpdateRoad();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Place Buildings"))
        {
            Undo.RecordObject(creator, "Placed buildings");
            creator.PlaceBuildings();
        }

        if (GUILayout.Button("Finalize")){
            Undo.RecordObject(creator, "Finalize track");
            creator.FinalizePath();

            //set objects static for static batching. rendering optimization technique
            GameObjectUtility.SetStaticEditorFlags(creator.filter.gameObject, StaticEditorFlags.BatchingStatic);
            GameObjectUtility.SetStaticEditorFlags(creator.gameObject, StaticEditorFlags.BatchingStatic);
            GameObjectUtility.SetStaticEditorFlags(creator.gameObject, StaticEditorFlags.OccludeeStatic);
            GameObjectUtility.SetStaticEditorFlags(creator.gameObject, StaticEditorFlags.OccluderStatic);
            GameObjectUtility.SetStaticEditorFlags(creator.gameObject, StaticEditorFlags.LightmapStatic);

            GameObject g = creator.gameObject;

            //save track with mesh to to scriptable object
            LevelData level = Resources.Load<LevelData>(g.name);

            EditorUtility.SetDirty(level);

            //set leveldata variables
            level.points = creator.points;
            level.roadWidth = creator.roadWidth;
            level.textureTiling = creator.tiling;
            level.pointSpacing = creator.spacing;

            //remove unnecessary components in preperation of prefab creation
            DestroyImmediate(g.GetComponent<RoadCreator>());
            DestroyImmediate(g.GetComponent<PathCreator>());
            DestroyImmediate(g.GetComponent<PathPlacer>());

            g.AddComponent<ObstacleHelper>();

            //create prefab & set last leveldata property
            GameObject t = PrefabUtility.CreatePrefab("Assets/Resources/" + g.name + "Prefab.prefab", g);
            level.levelObject = t;
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }

    private void OnEnable(){
        creator = (RoadCreator)target;
    }
}