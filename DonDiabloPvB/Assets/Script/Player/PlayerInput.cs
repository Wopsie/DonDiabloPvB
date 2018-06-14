using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all Input data used on Obstacles Tap and Hold.
/// </summary>
public class PlayerInput : MonoBehaviour {

    private bool buttonPressed;
    //private int heldFrameCounter;

    public delegate void PressButton(ShieldState state);
    public PressButton OnPressButton;

    public delegate void HoldButton(ShieldState state );
    public HoldButton OnHoldButton;

    public delegate void ReleaseButton(ShieldState state);
    public ReleaseButton OnReleaseButton;

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            if(OnPressButton != null){
                OnPressButton(ShieldState.TapShield);
            }
            //pressed key
            //heldFrameCounter = 1;
            buttonPressed = true;
        }

        if (Input.GetMouseButtonUp(0)){
            if(OnReleaseButton != null){
                OnReleaseButton(ShieldState.NoShield);
                //Debug.Log("BUTTON RELEASED");
            }

            buttonPressed = false;
            //released key
            //Debug.Log(heldFrameCounter);
        }

        if (Input.GetMouseButton(0)){
            if (buttonPressed){

                if(OnHoldButton != null){
                    OnHoldButton(ShieldState.HoldShield);
                }

                //heldFrameCounter++;
            }
        }
    }
}
