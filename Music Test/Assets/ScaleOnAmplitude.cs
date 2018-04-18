using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour {

    public float _startScale, _MaxScale;
    public bool _useBuffer;
    Material _material;
    public float _red, _greed, _blue;

	// Use this for initialization
	void Start () {
        _material = GetComponent<MeshRenderer>().materials[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (!_useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer._Amplitude * _MaxScale) + _startScale, (AudioPeer._Amplitude * _MaxScale) + _startScale, (AudioPeer._Amplitude * _MaxScale));
               }

        else if (_useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer._AmplitudeBuffer * _MaxScale) + _startScale, (AudioPeer._AmplitudeBuffer * _MaxScale) + _startScale, (AudioPeer._AmplitudeBuffer * _MaxScale));
        }
    }
}
