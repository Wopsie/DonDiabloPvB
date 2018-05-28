using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private LevelData level;

    private void Start(){

        LevelData level = Resources.Load<LevelData>("TestLevel");
        Debug.Log(level);
        Instantiate(level.levelObject);
    }
}
