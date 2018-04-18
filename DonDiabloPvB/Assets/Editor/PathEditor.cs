using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor {

    PathCreator creator;
    Path path;

    private void OnSceneGUI()
    {
        Draw();
        Input();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        //Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;



        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            /*
            Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("JA");
                Undo.RecordObject(creator, "Add Segment");
                //mousePos.z += 3;
                Debug.Log(hit.point.x + " " + hit.point.y + " " + hit.point.z);
                path.AddSegment(hit.point);
            }
            else
            {
                Debug.Log("nothing hit");
                Debug.Log(hit.distance);
            }
            */

            Undo.RecordObject(creator, "Add Segment");
            mousePos.z -= 3;
            path.AddSegment(guiEvent.mousePosition);
            
        }
    }

    void Draw(){

        for (int i = 0; i < path.NumSegments; i++){
            Vector3[] points = path.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; i++){
            Vector3 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
            if(path[i] != newPos){
                Undo.RecordObject(creator, "Move Point");
                path.MovePoint(i, newPos);
            }
        }
    }

    private void OnEnable(){
        creator = (PathCreator)target;
        if(creator.path == null){
            creator.CreatePath();
        }
        path = creator.path;
    }
}
