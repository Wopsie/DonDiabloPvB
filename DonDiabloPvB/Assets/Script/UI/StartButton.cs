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

    [SerializeField] private GameObject _parent;
    [SerializeField] private Text levelNumberText;
    [SerializeField] private GameObject _menuUI;

    private AudioSource _audioSource;
    private LevelManager loader;
    private int _level;

    void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        loader = FindObjectOfType<LevelManager>();
    }

    public void ButtonSelect()
    {
        _audioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        SettingsHandler.Instance.SetButtonActive(true);
        _menuUI.SetActive(false);
        _parent.SetActive(false);
        
    }

    public void PassLevelNumber(int levelNumber)
    {
        _level = levelNumber;
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }

    void ChangeLevelScreen()
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