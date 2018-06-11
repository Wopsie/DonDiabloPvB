using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private int songNumber;
    private int score;
    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(wait());
        score = 0;
	}

    //Adds score with given amount
    void AddScore(int amountGain = 1)
    {
        score += amountGain;
        text.text = score.ToString();
    }

    //Only for testing score it adds 1 to score each second
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        AddScore();
        Progress.SetProgress(songNumber.ToString(), score);
        StartCoroutine(wait());
    }
}
