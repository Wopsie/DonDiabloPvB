﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineEditor { 

    public class PathCreator : MonoBehaviour {

        [HideInInspector]
	    public Path path;

        public void CreatePath(){
            path = new Path(transform.position);
        }
    }
}
