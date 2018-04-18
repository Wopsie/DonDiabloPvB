using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateCubes : MonoBehaviour {
    public GameObject _samplePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 512; i++)
        {
            GameObject _instaceSampleCube = (GameObject)Instantiate(_samplePrefab);
            _instaceSampleCube.transform.position = this.transform.position;
            _instaceSampleCube.transform.parent = this.transform;
            _instaceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instaceSampleCube.transform.position = Vector3.forward * 100;
            _sampleCube[i] = _instaceSampleCube;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 512; i++)
        {
            if (_sampleCube != null) {
                _sampleCube[i].transform.localScale = new Vector3(1, (AudioPeer._samples[i] * _maxScale) + 2, 1);
            }
        }
	}
}
