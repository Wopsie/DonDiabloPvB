using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    [SerializeField] private GameObject _menuUI;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private NewPlayerMovement _playermovement;

    private int _level;

    private SpriteRenderer _spriteRenderer;
    private Collider _collider;

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();


    private void Start()
    {
        _collider = this.gameObject.GetComponent<Collider>();
        _collider.enabled = false;
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        ChangeLevelScreen();
    }

    void OnMouseDown()
    {
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
        _collider.enabled = true;
    }

    public void ToChangeLevel()
    {
        ChangeLevelScreen();
    }
}
