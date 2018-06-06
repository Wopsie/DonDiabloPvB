using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    public static StartButton Instance { get { return GetInstance(); } }

    #region Singleton
    private static StartButton instance;

    private static StartButton GetInstance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<StartButton>();
        }

        return instance;
    }
    #endregion

    [SerializeField] private GameObject _menuUI;
    [SerializeField] private Text levelNumberText;

    private AudioSource _audioSource;
    private LevelManager loader;
    private int _level;

    public void ButtonSelect()
    {
        //string levelName = "Level" + _level.ToString();
        //loader.PlaceLevel(levelName);
        Debug.Log(_level);
        _audioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        _menuUI.SetActive(false);
    }

    public void PassLevelNumber(int levelNumber)
    {
        _level = levelNumber;
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }

    private void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        loader = FindObjectOfType<LevelManager>();
    }

    private void ChangeLevelScreen()
    {
        PassLevelNumber(_level);

        if (_level >= 0)
        {
            Debug.Log("Passed valid number");
            levelNumberText.text = _level.ToString();
        }
        else
        {
            Debug.Log("Passed invalid number");
            levelNumberText.text = "No level selected";
        }
    }
}