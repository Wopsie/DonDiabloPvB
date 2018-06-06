using UnityEngine;

public class LevelManager : MonoBehaviour {

    GameObject g;
    string replayLevelName;

    public void PlaceLevel(string levelName){
        Debug.Log(levelName);
        LevelData level = Resources.Load<LevelData>(levelName);
        g = Instantiate(level.levelObject);
        replayLevelName = levelName;
    }

    public void ReloadLevel(){
        LevelData level = (replayLevelName != "") ? Resources.Load<LevelData>(replayLevelName) : Resources.Load<LevelData>("Level");
        g = Instantiate(level.levelObject);
    }

    /// <summary>
    /// method handels the removal of the currently instantiated level
    /// </summary>
    public void RemoveLevel(){
        Destroy(g);
    }
}
