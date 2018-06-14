using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SplineEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// RoadEditor which finalize all data set of point,road width,texture tilling,point spacing.
/// </summary>

[CustomEditor(typeof(RoadCreator))]
public class RoadEditor : Editor{
    RoadCreator creator;

    private void OnSceneGUI(){
        if (creator.autoUpdate && Event.current.type == EventType.Repaint){
            creator.UpdateRoad();
        }
    }

    /// <summary>
    /// Instantiate all data of the road of point,road width,texture tilling,point spacing.
    /// </summary>
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        if(GUILayout.Button("Place Buildings")){
            Undo.RecordObject(creator, "Placed buildings");
            creator.PlaceBuildings();
        }

        if(GUILayout.Button("Clean Scene")){
            creator.CleanScene();
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

            //Add a script that upon instantiation searches for obstacle helper & player movement upon instantiation
            g.AddComponent<LevelStarter>();

            //create prefab of the level track
            GameObject t = PrefabUtility.CreatePrefab("Assets/Resources/" + g.name + "Prefab.prefab", g);
            level.levelTrackObj = t;

            //create prefab of the background elements
            t = PrefabUtility.CreatePrefab("Assets/Resources/" + g.name + "Background" + "Prefab.prefab", creator.backgroundElementsGo);
            level.levelBackgroundObj = t;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }

    private void OnEnable(){
        creator = (RoadCreator)target;
    }
}