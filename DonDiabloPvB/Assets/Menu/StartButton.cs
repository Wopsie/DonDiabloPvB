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

    private AudioSource _audioSource;
    private LevelManager loader;
    private int _level;
    private Image _imageRenderer;

    public void ButtonSelect()
    {
        //loader.PlaceLevel("LevelFour");
        Debug.Log(_level);
        _audioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        _menuUI.SetActive(false);
    }

    public void LevelNumber(int levelNumber)
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
        _imageRenderer = this.gameObject.GetComponent<Image>();
        loader = FindObjectOfType<LevelManager>();
    }

    private void ChangeLevelScreen()
    {
        _imageRenderer.sprite = _levelSelectScreens[_level];
    }
}