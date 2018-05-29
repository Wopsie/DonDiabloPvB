using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public void PlaceLevel(string levelName){
        LevelData level = Resources.Load<LevelData>(levelName);
        Instantiate(level.levelObject);
    }
}
