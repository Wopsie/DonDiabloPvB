using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private LevelData level;

    private void Awake(){

        LevelData level = Resources.Load<LevelData>("LevelOne");
        //Debug.Log(level);
        Instantiate(level.levelObject);
    }
}
