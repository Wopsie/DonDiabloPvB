using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obstacle base class
/// </summary>
public class Obstacle : MonoBehaviour {

    //obstacles share certain properties, this class allows us to set defaults for every obstacle if we are not sure about certain unique details

    //every obstacle has a type
    //every obstacle has access to ObstacleHelper (still thinking about optimal way to do this)
    //every obstacle has particle when they get destroyed
    //every obstacle has sound effect for destroy
    //every obstacle has particle/animation for "initiation"
    //every obstacle has colour shift as player get closer (?)

    protected enum ObstacleType{
        Tap,
        Hold,
    };

}
