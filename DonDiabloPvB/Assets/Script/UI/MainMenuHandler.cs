using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// MainMenuHandler as it say it handles the main menu from choosing song to starting game.
/// </summary>
public class MainMenuHandler : MonoBehaviour {
    #region Singleton
    private static MainMenuHandler instance;

    private static MainMenuHandler GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<MainMenuHandler>();
        }

        return instance;
    }
    #endregion
    public static MainMenuHandler Instance { get { return GetInstance(); } }

    public AudioSource AudioSource;
    public GameObject UIStartButton;
    public GameObject startButton;
    public GameObject SelectButtons;
    public int LevelNumber;    
    private Text LevelNumberText;

    private void Awake()
    {
        AudioSource = Camera.main.GetComponent<AudioSource>();
        SelectButtons = transform.GetChild(0).gameObject;
        UIStartButton = GameObject.FindGameObjectWithTag("UIStartButton");
        startButton = GameObject.FindGameObjectWithTag("StartButton");
        LevelNumberText = startButton.transform.GetChild(0).GetComponent<Text>();
        UIStartButton.SetActive(false);
    }

    /// <summary>
    /// Sets number of song so other script can read it like score to save score to playerprefs when level is completed.
    /// </summary>
    /// <param name="number"></param>
    public void SetLevelNumber(int number)
    {
        LevelNumber = number;
    }
    /// <summary>
    /// Sets audioclip called by SelectButton.
    /// </summary>
    /// <param name="_audioClip"></param>
    public void SetAudioClip(AudioClip _audioClip)
    {
        AudioSource.clip = _audioClip;
    }
    /// <summary>
    /// When in Main menu and you selected a song shader closes and opens, the level number will set in meantime.
    /// </summary>
    public void ChangeLevelScreen()
    {
        if (LevelNumber == 0)
        {
            LevelNumberText.text = "Level Not Selected";
        }
        else
        {
            LevelNumberText.text = LevelNumber.ToString();
        }
    }

}
