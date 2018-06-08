using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject MenuUI;
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

    public void SetLevelNumber(int number)
    {
        LevelNumber = number;
    }

    public void SetAudioClip(AudioClip _audioClip)
    {
        AudioSource.clip = _audioClip;
    }

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
