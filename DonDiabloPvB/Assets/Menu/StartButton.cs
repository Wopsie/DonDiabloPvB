using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    [SerializeField] private GameObject _menuUI;
    private AudioSource _audioSource;
    private LevelManager loader;

    [SerializeField] private ShaderController _shaderController;

    private int _level;

    private Image _imageRenderer;

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();


    void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        _imageRenderer = this.gameObject.GetComponent<Image>();
        loader = FindObjectOfType < LevelManager>();
    }

    public void ButtonSelect()
    {
        //loader.PlaceLevel("LevelFour");
        Debug.Log(_level);
        _audioSource.Play();
        _shaderController.TriggerEffect(1);
        _menuUI.SetActive(false);
    }

    public void LevelNumber(int levelNumber)
    {
        _level = levelNumber;
    }


    private void ChangeLevelScreen()
    {
        _imageRenderer.sprite = _levelSelectScreens[_level];
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }
}
