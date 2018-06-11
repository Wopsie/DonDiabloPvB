using UnityEngine;

/// <summary>
/// Saves all progress in completing songs in game.
/// </summary>
public class Progress {

    /// <summary>
    /// If new score more is than old score function is called.
    /// name and score are give to check if it happend
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public static void SetProgress(string name,int score) {
        if (PlayerPrefs.GetInt(name) < score)
        {
            PlayerPrefs.SetInt(name, score);
        }
    }

    /// <summary>
    /// Get current highest score of song with given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static int GetProgress(string name) {
        int i = PlayerPrefs.GetInt(name);
        return i;
    }
}
