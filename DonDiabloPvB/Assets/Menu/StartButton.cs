﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    [SerializeField] private GameObject _menuUI;
    private AudioSource _audioSource;
    [SerializeField] private NewPlayerMovement _playermovement;
    private LevelLoader loader;

    private int _level;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();


    void Awake()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        loader = FindObjectOfType<LevelLoader>();
    }

    public void ButtonSelect()
    {
        loader.PlaceLevel("LevelFour");
        Debug.Log(_level);
        _audioSource.Play();
        _playermovement.enabled = true;
        _menuUI.SetActive(false);
    }

    public void LevelNumber(int levelNumber)
    {
        _level = levelNumber;
    }


    private void ChangeLevelScreen()
    {
        _spriteRenderer.sprite = _levelSelectScreens[_level];
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }
}
