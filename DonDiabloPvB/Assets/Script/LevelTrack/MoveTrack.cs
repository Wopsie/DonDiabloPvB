using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class moves the track towards the player
/// in greybox phase
/// </summary>
public class MoveTrack : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;
    [Range(0.1f, 5)][SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float maxSpeed = 5;

    private void Awake(){
        rb.maxAngularVelocity = 100;
    }

    private void FixedUpdate(){
        //move track
        rb.AddForce(-Vector3.forward * speed);
        if(rb.velocity.z <= -5){
            rb.velocity = -Vector3.forward * maxSpeed;
        }
    }
}
