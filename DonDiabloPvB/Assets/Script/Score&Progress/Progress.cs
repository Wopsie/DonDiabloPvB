using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Progress : MonoBehaviour {
    [SerializeField]
    private Score scoreScript;
    [SerializeField]
    private List<AudioClip> songs = new List<AudioClip>();
    public AudioClip[] temp;
    public string[] a;
    public string[] songNames;

	void Awake () {
        temp = Resources.LoadAll<AudioClip>("Audio");
        songNames = new string[temp.Length];
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
