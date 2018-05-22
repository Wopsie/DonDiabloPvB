using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Progress : MonoBehaviour {
    [SerializeField]
    private Score scoreScript;
    [SerializeField]
    void Awake()
    {
        scoreScript = GetComponent<Score>();
    }
    // Update is called once per frame
    void Update () {

	}

    void UpdateProgress(string name,int score) {
        PlayerPrefs.SetInt(name, score);
    }

    int GetProgress(string name) {
        int i = PlayerPrefs.GetInt(name);
        return i;
    }
}
