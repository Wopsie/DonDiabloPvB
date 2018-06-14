using UnityEngine;

/// <summary>
/// LevelManager instantiate the level which is chosen in main menu.
/// </summary>
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

    public LevelData level;
    private GameObject g;
    private string replayLevelName;
    /// <summary>
    /// Instantiate level and is called by startbutton when level is chosen.
    /// </summary>
    /// <param name="levelName"></param>
    public void PlaceLevel(string levelName){
        Debug.Log(levelName);
        level = Resources.Load<LevelData>(levelName);
        g = Instantiate(level.levelTrackObj, Vector3.zero, Quaternion.identity);
        replayLevelName = levelName;
    }
    /// <summary>
    /// Reloads the current instatiated level.
    /// </summary>
    public void ReloadLevel(){
        LevelData level = (replayLevelName != "") ? Resources.Load<LevelData>(replayLevelName) : Resources.Load<LevelData>("Level");
        g = Instantiate(level.levelTrackObj);
    }

    /// <summary>
    /// method handels the removal of the currently instantiated level
    /// </summary>
    public void RemoveLevel(){
        Destroy(g);
    }
}
