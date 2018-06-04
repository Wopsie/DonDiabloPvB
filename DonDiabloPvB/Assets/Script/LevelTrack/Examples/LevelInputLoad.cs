using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInputLoad : MonoBehaviour {

    private LevelManager loader;

    private void Awake(){
        loader = GetComponent<LevelManager>();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha0)){
            loader.PlaceLevel("TestLevel");
        }else if (Input.GetKeyDown(KeyCode.Alpha1)){
            loader.PlaceLevel("LevelOne");
        }else if (Input.GetKeyDown(KeyCode.Alpha2)){
            loader.PlaceLevel("LevelTwo");
        }else if (Input.GetKeyDown(KeyCode.Alpha3)){
            loader.PlaceLevel("LevelThree");
        }else if (Input.GetKeyDown(KeyCode.Alpha4)){
            loader.PlaceLevel("LevelFour");
        }
    }
}
