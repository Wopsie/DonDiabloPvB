using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    private int _level;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();


    private void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        Debug.Log(_level);
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
