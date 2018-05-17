using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineEditor { 

    public class PathCreator : MonoBehaviour {

        [HideInInspector]
	    public Path path;
        public Color anchorCol = Color.red;
        public Color controlColor = Color.white;
        public Color segmentCol = Color.green;
        public Color selectSegCol = Color.yellow;
        public float anchorDia = 0.1f;
        public float controlDia = 0.075f;
        public bool displayCntrlPoints = true;
        public bool displayPoints = true;

        public void CreatePath(){
            path = new Path(transform.position);
        }
    }
}
