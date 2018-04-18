using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {
    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    public bool _testBeat;
    public float _beatsensivity;
    private float _old = 0, _new;
    Material _material;
	// Use this for initialization
	void Start () {
        _material = GetComponent<MeshRenderer>().materials[0];
	}

    void TestBeat()
    {
        if (_testBeat)
        {
            _new = AudioPeer._bandBuffer[_band];
            if (_new - _old > _beatsensivity)
            {
                Debug.Log("Beat");
            }
            _old = _new;
        }
    }

	// Update is called once per frame
	void Update () {
        
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            Color _color = new Color(AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band]);
            _material.SetColor("EmissionColor", _color);
        }
        else if (!_useBuffer) {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            Color _color = new Color(AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band]);
            _material.SetColor("EmissionColor", _color);
        }
        TestBeat();
	}
}
