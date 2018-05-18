using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    private string songName;
    public int highScore;
    private int amount = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetSong(string name = "0")
    {
        songName = name;
    }

    void AddScore()
    {
        highScore = highScore + amount;
    }
}
