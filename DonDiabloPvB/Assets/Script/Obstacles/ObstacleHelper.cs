using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ensures that the obstacles are placed on the track properly & ensures the right obstacles are enabled in the right order
/// </summary>
public class ObstacleHelper : MonoBehaviour {

    //when placing obstacles in scene, check what trackpoint is closest & snap obstacles position to that point
    //rotate obstacle correctly in direction of next waypoint
        //alternatively i can rotate the obstacle in the direction of one of the mesh edges & add/subtract 90 degrees from it idk yet
    //editor script might be required & is probably not necessary for demo on thursday

    //class keeps track of what waypoints have been passed by player
    //it compares this number to the number of the waypoint that the obstacle is positioned at
	
}
