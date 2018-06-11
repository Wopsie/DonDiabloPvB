using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles score and saves it to Playerprefs by number of song.
/// </summary>
public class Score : MonoBehaviour {
    private int songNumber;
    private int score;
    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        score = 0;
	}
    /// <summary>
    /// Adds score with given amount to text.
    /// </summary>
    void AddScore(int amountGain = 1)
    {
        score += amountGain;
        text.text = score.ToString();
    }

}
