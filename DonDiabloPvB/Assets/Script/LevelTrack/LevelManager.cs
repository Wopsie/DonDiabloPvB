using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    GameObject g;

    public void PlaceLevel(string levelName){
        LevelData level = Resources.Load<LevelData>(levelName);
        g = Instantiate(level.levelObject);
    }

    /// <summary>
    /// method handels the removal of the currently instantiated level
    /// </summary>
    public void RemoveLevel(){
        Destroy(g);
    }
}
