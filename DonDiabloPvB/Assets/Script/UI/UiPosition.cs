using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPosition : MonoBehaviour {

    [SerializeField] private Transform _targetTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = _targetTransform.position;
        this.gameObject.transform.rotation = _targetTransform.rotation;
    }
}
