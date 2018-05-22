using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private string songName;
    private int currentScore;
    public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    [SerializeField]
    private Text text;
	// Use this for initialization
	void Start () {
        StartCoroutine(wait());
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
        currentScore = currentScore + 1;
        text.text = currentScore.ToString();
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        AddScore();
        StartCoroutine(wait());
    }
}
