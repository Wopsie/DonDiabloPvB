using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private bool buttonPressed;
    private int heldFrameCounter;

    public delegate void PressButton();
    public PressButton OnPressButton;

    public delegate void HoldButton();
    public HoldButton OnHoldButton;

    public delegate void ReleaseButton();
    public ReleaseButton OnReleaseButton;

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            if(OnPressButton != null){
                OnPressButton();
            }
            //pressed key
            heldFrameCounter = 1;
            buttonPressed = true;
        }

        if (Input.GetMouseButtonUp(0)){
            if(OnReleaseButton != null){
                OnReleaseButton();
            }

            buttonPressed = false;
            //released key
            Debug.Log(heldFrameCounter);
        }

        if (Input.GetMouseButton(0)){
            if (buttonPressed){

                if(OnHoldButton != null){
                    OnHoldButton();
                }

                heldFrameCounter++;
            }
        }
    }
}
