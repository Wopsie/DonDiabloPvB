using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SplineEditor
{
    [CustomEditor(typeof(PathCreator))]
    public class PathEditor : Editor {

        PathCreator creator;
        Path Path {
            get { return creator.path; }
        }

        const float segmentSelectDistanceThres = .1f;
        int selectedSegmentIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            if (GUILayout.Button("Create New")){
                Undo.RecordObject(creator, "Create new");
                StartUpWindow.ShowWindow();
                StartUpWindow.CreateFileEvent.AddListener(SetObjectName);
                creator.CreatePath();
            }

            //bool isClosed = GUILayout.Toggle(Path.IsClosed, "Closed");
            bool isClosed = false;
            if (isClosed != Path.IsClosed){
                Undo.RecordObject(creator, "Toggle closed");
                Path.IsClosed = isClosed;
            }

            bool autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto set control points");
            if(autoSetControlPoints != Path.AutoSetControlPoints){
                Undo.RecordObject(creator, "Toggle Auto set controls");
                Path.AutoSetControlPoints = autoSetControlPoints;
            }

            if (EditorGUI.EndChangeCheck()){
                SceneView.RepaintAll();
            }
        }

        private void OnSceneGUI(){
            Draw();
            PathInput();
        }

        void SetObjectName(string s){
            Debug.Log("SET NAME OF OBJ TO " + s);
            creator.gameObject.name = s;
        }

        void PathInput()
        {
            Event guiEvent = Event.current;
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift){
                if(selectedSegmentIndex != -1){
                    Undo.RecordObject(creator, "Split Segment");
                    Path.SplitSegment(mousePos, selectedSegmentIndex);
                }else if(!Path.IsClosed){
                    Undo.RecordObject(creator, "Add Segment");
                    Path.AddSegment(mousePos);
                }
            }

            if(guiEvent.type == EventType.MouseDown && guiEvent.button == 1){
                float minDistToAnchor = creator.anchorDia * 0.5f;
                int closestAnchorIndex = -1;

                for (int i = 0; i < Path.NumPoints; i+=3){
                    float dst = Vector2.Distance(mousePos, Path[i]);
                    if(dst < minDistToAnchor){
                        minDistToAnchor = dst;
                        closestAnchorIndex = i;
                    }
                }

                if(closestAnchorIndex != -1){
                    Undo.RecordObject(creator, "Delete segment");
                    Path.DeleteSegment(closestAnchorIndex);
                }
            }

            if(guiEvent.type == EventType.MouseMove){
                float minDistToSeg = segmentSelectDistanceThres;
                int newSelectedSegIndex = -1;

                for (int i = 0; i < Path.NumSegments; i++){
                    Vector2[] points = Path.GetPointsInSegment(i);
                    float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                    if(dst < minDistToSeg){
                        minDistToSeg = dst;
                        newSelectedSegIndex = i;
                    }
                }

                if(newSelectedSegIndex != selectedSegmentIndex){
                    selectedSegmentIndex = newSelectedSegIndex;
                    HandleUtility.Repaint();
                }
            }

            HandleUtility.AddDefaultControl(0);
        }

        void Draw(){
            if (creator.displayPoints)
            {
                for (int i = 0; i < Path.NumSegments; i++)
                {
                    Vector2[] points = Path.GetPointsInSegment(i);
                    if (creator.displayCntrlPoints)
                    {
                        Handles.color = Color.black;
                        Handles.DrawLine(points[1], points[0]);
                        Handles.DrawLine(points[2], points[3]);
                    }
                    Color segmentColor = (i == selectedSegmentIndex && Event.current.shift) ? creator.selectSegCol : creator.segmentCol;
                    Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentColor, null, 2);
                }

                for (int i = 0; i < Path.NumPoints; i++)
                {
                    if (i % 3 == 0 || creator.displayCntrlPoints)
                    {
                        Handles.color = (i % 3 == 0) ? creator.anchorCol : creator.controlColor;
                        float handleSize = (i % 3 == 0) ? creator.anchorDia : creator.controlDia;
                        Vector2 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
                        if (Path[i] != newPos)
                        {
                            Undo.RecordObject(creator, "Move Point");
                            Path.MovePoint(i, newPos);
                        }
                    }
                }
            }
        }

        private void OnEnable(){
            creator = (PathCreator)target;
            if(creator.path == null){
                creator.CreatePath();
            }
        }
    }
}
