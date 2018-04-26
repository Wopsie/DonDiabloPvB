using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private PlayerInput input;

    private void Start(){
        input = GetComponent<PlayerInput>();
        input.OnPressButton += GetPress;
        input.OnHoldButton += GetHold;
        input.OnReleaseButton += GetRelease;
    }

    void GetPress(){
        Debug.Log("PRESS");
    }

    void GetHold(){
        Debug.Log("HOLD");
    }

    void GetRelease(){
        Debug.Log("RELEASE");
    }

    //

}
