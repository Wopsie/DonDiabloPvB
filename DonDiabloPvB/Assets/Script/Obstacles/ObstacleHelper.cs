using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ensures that the obstacles are placed on the track properly & ensures the right obstacles are enabled in the right order
/// </summary>
public class ObstacleHelper : MonoBehaviour {

    private Obstacle[] obstacleColl = null;
    [HideInInspector]
    public NewPlayerMovement player = null;
    [HideInInspector]
    public int playerPassIndex;
    private float maxScore;
    public float MaxScore { get { return maxScore; } }
    private float currentScore;
    public float CurrentScore { get { return currentScore; } }
    private Text scoreText;

    private void Awake(){
        obstacleColl = FindObjectsOfType<Obstacle>();
        for (int i = 0; i < obstacleColl.Length; i++){
            obstacleColl[i].ReceiveHelper(this);
            maxScore += obstacleColl[i].scoreToAward;
        }
        player = FindObjectOfType<NewPlayerMovement>();
        scoreText = GameObject.FindGameObjectWithTag(Tags.ScoreTextTag).GetComponent<Text>();
    }

    private void Update(){
        if (playerPassIndex != player.CurrWaypointIndex)
            playerPassIndex = player.CurrWaypointIndex;
    }

    public void AddScore(float score){
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }

    public Obstacle[] GetAllObstacles()
    {
        return obstacleColl;
    }

    //when placing obstacles in scene, check what trackpoint is closest & snap obstacles position to that point
    //rotate obstacle correctly in direction of next waypoint
    //alternatively i can rotate the obstacle in the direction of one of the mesh edges & add/subtract 90 degrees from it idk yet
    //editor script might be required & is probably not necessary for demo on thursday

    //class keeps track of what waypoints have been passed by player
    //it compares this number to the number of the waypoint that the obstacle is positioned at

}
