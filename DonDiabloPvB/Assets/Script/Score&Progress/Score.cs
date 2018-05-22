using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private string songName;
    private int currentScore;
   // public int CurrentScore { get { return currentScore; } set { currentScore = value; } }
    [SerializeField]
    private Text text;
	// Use this for initialization
	void Start () {
        StartCoroutine(wait());
        currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetSong(string name = "0")
    {
        songName = name;
    }

    void AddScore(int amountGain = 1)
    {
        currentScore += amountGain;
        Debug.Log(amountGain);
        text.text = currentScore.ToString();
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        AddScore();
        StartCoroutine(wait());
    }
}
