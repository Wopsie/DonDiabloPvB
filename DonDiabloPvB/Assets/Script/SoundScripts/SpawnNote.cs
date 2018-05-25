using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour {
    public GameObject obj;
    public bool PlayMusic;
    private AudioSource source;
    private float beginNote;
    private int currentNote = 0;
	// Use this for initialization
	void Start () {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (PlayMusic && !source.isPlaying)
        {
            source.Play();
        }
        else if (source.isPlaying && !PlayMusic)
        {
            source.Stop();
            currentNote = 0;
        }
        else if (source.isPlaying)
        {
            Spawn();
        }
	}

    void Spawn()
    {
        if (source.time + 1f > NoteDetection.timeDetected[currentNote])
        {
            Instantiate(obj);
            currentNote++;
            Debug.Log("BEAT");
        }
        
    }
}
