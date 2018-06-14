using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineEditor
{
    /// <summary>
    /// Initate and handles everything of the Path/Track.
    /// </summary>
    [System.Serializable]
    public class Path {

        [SerializeField, HideInInspector]
        private List<Vector2> points;

        [SerializeField, HideInInspector]
        private bool isClosed;

        [SerializeField, HideInInspector]
        private bool autoSetControlPoints;

        public Path(Vector2 centre) {
            points = new List<Vector2>{
                centre+Vector2.left,
                centre+(Vector2.left + Vector2.up)*10f,
                centre+(Vector2.left + Vector2.up)* 30f,
                centre+(Vector2.left + Vector2.up) * 50,
            };
        }

        public Vector2 this[int i]{
            get { return points[i]; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set {
                if (isClosed != value){
                    isClosed = value;

                    if (isClosed){
                        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                        points.Add(points[0] * 2 - points[1]);
                        if (autoSetControlPoints){
                            AutoSetAnchorControlPoints(0);
                            AutoSetAnchorControlPoints(points.Count - 3);
                        }
                    }else{
                        points.RemoveRange(points.Count - 2, 2);
                        if (autoSetControlPoints){
                            AutoSetStartEndControls();
                        }
                    }
                }
            }
        }

        public bool AutoSetControlPoints {
            get { return autoSetControlPoints; }
            set {
                if (autoSetControlPoints != value) {
                    autoSetControlPoints = value;
                    if (autoSetControlPoints) {
                        AutoSetAllCntrlPoints();
                    }
                }
            }
        }

        public int NumPoints { get { return points.Count; } }

        public int NumSegments {
            get { return points.Count / 3; }
        }
        /// <summary>
        /// Instatiate a Path segment.
        /// </summary>
        /// <param name="anchorPos">The position you want to have the segment.</param>
        public void AddSegment(Vector2 anchorPos){
            points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
            points.Add((points[points.Count - 1] + anchorPos) * .5f);
            points.Add(anchorPos);

            if (autoSetControlPoints) {
                AutoSetAllAffected(points.Count - 1);
            }
        }
        /// <summary>
        /// Splits the segment
        /// </summary>
        /// <param name="anchorPos">Position of segment</param>
        /// <param name="segmentIndex">Index of segment in array.</param>
        public void SplitSegment(Vector2 anchorPos, int segmentIndex){
            points.InsertRange(segmentIndex * 3 + 2, new Vector2[] { Vector2.zero, anchorPos, Vector2.zero });
            if (autoSetControlPoints){
                AutoSetAllAffected(segmentIndex * 3 + 3);
            }else{
                AutoSetAnchorControlPoints(segmentIndex * 3 + 3);
            }
        }
        /// <summary>
        /// Deletes whole segment from array
        /// </summary>
        /// <param name="anchorIndex">Index of segment in array to delete.</param>
        public void DeleteSegment(int anchorIndex){
            if(NumSegments > 2 || !isClosed && NumSegments > 1){
                //first point in spline
                if (anchorIndex == 0){
                    if (isClosed){
                        points[points.Count - 1] = points[2];
                    }
                    points.RemoveRange(0, 3);
                }//last point in list
                else if(anchorIndex == points.Count - 1 && !isClosed){
                    points.RemoveRange(anchorIndex - 2, 3);
                }else{
                    points.RemoveRange(anchorIndex - 1, 3);
                }
            }
        }
        /// <summary>
        /// Get point from array of segment.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns></returns>
        public Vector2[] GetPointsInSegment(int i){
            return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[LoopIndex(i * 3 + 3)] };
        }
        /// <summary>
        /// Moves segment.
        /// </summary>
        /// <param name="i">index of array.</param>
        /// <param name="pos">postion to move to.</param>
        public void MovePoint(int i, Vector2 pos){
            Vector2 deltaMove = pos - points[i];

            if(i % 3 == 0 || !autoSetControlPoints) { 
            points[i] = pos;

                if (autoSetControlPoints){
                    AutoSetAllAffected(i);
                }else{
                    if (i % 3 == 0)
                    {
                        if (i + 1 < points.Count || isClosed)
                            points[LoopIndex(i + 1)] += deltaMove;
                        if (i - 1 >= 0 || isClosed)
                            points[LoopIndex(i - 1)] += deltaMove;
                    }
                    else
                    {
                        bool nextPointIsAnchor = (i + 1) % 3 == 0;
                        //get the controlpoint you need to move, either the one before or after the anchor point you are influencing
                        int corrControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
                        int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

                        if (corrControlIndex >= 0 && corrControlIndex < points.Count || isClosed)
                        {
                            //record distance of controlpoint from its anchorpoint to make sure it stays at this distance
                            float dist = (points[LoopIndex(anchorIndex)] - points[LoopIndex(corrControlIndex)]).magnitude;

                            Vector2 dir = (points[LoopIndex(anchorIndex)] - pos).normalized;
                            points[LoopIndex(corrControlIndex)] = points[LoopIndex(anchorIndex)] + dir * dist;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Calculate if all segment have even spave between them.
        /// </summary>
        /// <param name="spacing">amount of spacing</param>
        /// <param name="resolution">resolution.</param>
        /// <returns></returns>
        public Vector2[] CalculateEvenSpacePoints(float spacing, float resolution = 1){
            List<Vector2> evenlySpacedPoints = new List<Vector2>();
            evenlySpacedPoints.Add(points[0]);
            Vector2 previousPoint = points[0];
            float dstSinceLastEventPoint = 0;

            for (int segmentIndex = 0; segmentIndex < NumSegments; segmentIndex++){
                Vector2[] p = GetPointsInSegment(segmentIndex);
                float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
                float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + controlNetLength / 2f;
                int divisions = Mathf.CeilToInt(estimatedCurveLength * resolution * 10);
                float t = 0;

                while(t <= 1){
                    t += 1f/divisions;
                    Vector2 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                    dstSinceLastEventPoint += Vector2.Distance(previousPoint, pointOnCurve);

                    while(dstSinceLastEventPoint >= spacing)
                    {
                        float overshootDst = dstSinceLastEventPoint - spacing;
                        Vector2 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overshootDst;
                        evenlySpacedPoints.Add(newEvenlySpacedPoint);
                        dstSinceLastEventPoint = overshootDst;
                        previousPoint = newEvenlySpacedPoint;
                    }
                    previousPoint = pointOnCurve;
                }
            }

            return evenlySpacedPoints.ToArray();
        }

        void AutoSetAllAffected(int updatedAnchorIndex){
            for (int i = updatedAnchorIndex-3; i <= updatedAnchorIndex + 3; i+= 3){
                if(i >=0 && i < points.Count || isClosed){
                    AutoSetAnchorControlPoints(LoopIndex(i));
                }
            }

            AutoSetStartEndControls();
        }

        void AutoSetAllCntrlPoints(){
            for (int i = 0; i < points.Count; i++){
                AutoSetAnchorControlPoints(i);
            }

            AutoSetStartEndControls();
        }

        void AutoSetAnchorControlPoints(int anchorIndex){
            Vector2 anchorPos = points[anchorIndex];
            Vector2 dir = Vector2.zero;
            float[] neightbourDistance = new float[2];

            //first neighbour
            if(anchorIndex - 3 >= 0 || isClosed){
                Vector2 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
                dir += offset.normalized;
                neightbourDistance[0] = offset.magnitude;
            }

            //second neighbour
            if (anchorIndex + 3 >= 0 || isClosed){
                Vector2 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
                dir -= offset.normalized;
                neightbourDistance[1] = -offset.magnitude;
            }

            dir.Normalize();

            for (int i = 0; i < 2; i++){
                int controlIndex = anchorIndex + i * 2 - 1;
                //check if out of range
                if (controlIndex >= 0 && controlIndex < points.Count || isClosed){
                    points[LoopIndex(controlIndex)] = anchorPos + dir * neightbourDistance[i] * .5f;
                }
            }
        }

        void AutoSetStartEndControls(){
            if (!isClosed){
                points[1] = (points[0] + points[2]) * .5f;
                points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * .5f;
            }
        }

        private int LoopIndex(int i){
            return (i + points.Count) % points.Count;
        }
    }
}

