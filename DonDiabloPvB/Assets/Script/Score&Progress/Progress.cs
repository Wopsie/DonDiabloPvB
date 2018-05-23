using UnityEngine;

public class Progress {

    public static void SetProgress(string name,int score) {
        if (PlayerPrefs.GetInt(name) < score)
        {
            PlayerPrefs.SetInt(name, score);
        }
    }

    public static int GetProgress(string name) {
        int i = PlayerPrefs.GetInt(name);
        return i;
    }
}
