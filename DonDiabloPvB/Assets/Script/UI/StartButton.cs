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

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private Text levelNumberText;

    private AudioSource _audioSource;
    private LevelManager loader;
    private int _level;
    private Image _imageRenderer;

    public void ButtonSelect()
    {
        string levelName = "Level" + _level.ToString();
        loader.PlaceLevel(levelName);
        Debug.Log(_level);
        _audioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        _menuUI.SetActive(false);
    }

    public void PassLevelNumber(int levelNumber)
    {
        _level = levelNumber;

        if (_level >= 0){
            Debug.Log("Passed valid number");
            levelNumberText.text = levelNumber.ToString();
        }else{
            Debug.Log("Passed invalid number");
            levelNumberText.text = "No level selected";
        }
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }

    private void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        _imageRenderer = this.gameObject.GetComponent<Image>();
        loader = FindObjectOfType<LevelManager>();
    }

    private void ChangeLevelScreen()
    {
        _imageRenderer.sprite = _levelSelectScreens[_level];
    }
}