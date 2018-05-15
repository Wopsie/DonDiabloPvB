using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetection : MonoBehaviour {
    private int count;
    private float newBuffer;
    private float oldBuffer;
    public float sensivity;
    [SerializeField]
    public static List<float> timeDetected = new List<float>();
    private AudioSource audioSource;
    public AudioClip audioClip;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Detect();
	}

    void Detect()
    {
        
        newBuffer = (AudioPeer._bandBuffer[0] + AudioPeer._bandBuffer[1]);
        if (count >= 2)
        {
            timeDetected.Add(audioSource.time);
            newBuffer = newBuffer / 2;
            count = 0;
        }
        if (newBuffer - oldBuffer > sensivity)
        {
            count++;
        }
        else
        {
            count = 0;
        }
        oldBuffer = newBuffer;
    }
}
