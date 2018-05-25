using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour {
    private Rigidbody rigid;
    private int Speed = -50;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.TransformDirection(new Vector3(0,0,Speed));
        StartCoroutine(Destroy());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
