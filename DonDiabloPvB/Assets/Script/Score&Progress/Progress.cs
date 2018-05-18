using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {
    private Score scoreScript;
	// Use this for initialization
	void Awake () {
        scoreScript = GameObject.Find("Score").GetComponent<Score>();
        UpdateProgress("1",scoreScript.highScore);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateProgress(string name,int score)
    {
        PlayerPrefs.SetInt(name, score);
        Debug.Log(PlayerPrefs.HasKey("0"));
    }
}
