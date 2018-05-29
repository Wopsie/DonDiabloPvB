using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private string songName;
    private int currentScore;
    public int CurrentScore { get { return currentScore; } }
    [SerializeField]
    private Text text;
	// Use this for initialization
	void Start () {
        StartCoroutine(wait());
        currentScore = 0;
	}

    //Adds score with given amount
    void AddScore(int amountGain = 1)
    {
        currentScore += amountGain;
        text.text = currentScore.ToString();
    }

    //Only for testing score it adds 1 to score each second
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        AddScore();
        Progress.SetProgress(songName, currentScore);
        StartCoroutine(wait());
    }
}
