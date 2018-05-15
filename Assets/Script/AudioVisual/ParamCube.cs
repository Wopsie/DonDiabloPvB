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
    private int count = 0;
    public GameObject obj;
    Material _material;
	// Use this for initialization
	void Start () {
        _material = GetComponent<MeshRenderer>().materials[0];
	}

    void TestBeat()
    {
        if (_testBeat)
        {
            _new = (AudioPeer._bandBuffer[2] + AudioPeer._bandBuffer[3]);
            if (count >= 2)
            {
                Debug.Log("beat");
                Instantiate(obj);
                count = 0;
            }
            if (_new - _old > _beatsensivity)
            {
                count++;
                Debug.Log("+1");
            }
            else
            {
                count = 0;
                Debug.Log("0");
            }
            _old = _new;
        }
    }

    IEnumerator Wait(float wait = 0)
    {
        yield return new WaitForSeconds(wait);
        _testBeat = true;
    }

	// Update is called once per frame
	void Update () {
        
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        }
        else if (!_useBuffer) {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        }
        TestBeat();
	}
}
