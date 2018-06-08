using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get { return GetInstance(); } }
    #region singleton
    private static LevelManager instance;
    private static LevelManager GetInstance(){
        if(instance == null){
            instance = FindObjectOfType<LevelManager>();
        }
        return instance;
    }
    #endregion

    [HideInInspector]
    public LevelData level;
    private GameObject g;
    private string replayLevelName;

    public void PlaceLevel(string levelName){
        Debug.Log(levelName);
        level = Resources.Load<LevelData>(levelName);
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
