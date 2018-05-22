using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    private int _level;

    private Collider _collider;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private List<Sprite> _levelSelectScreens = new List<Sprite>();


    private void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _collider = this.gameObject.GetComponent<Collider>();
    }

    //changes the number of the level
    public void LevelNumber(int levelNumber)
    {
        _level = levelNumber;
    }

    //changes the image of the level screen
    private void ChangeLevelScreen()
    {
        _spriteRenderer.sprite = _levelSelectScreens[_level];
    }

    //Gets orders from shader.
    public void ToChangeLevel(float Change)
    {
        if (Change == 1)
        {
            _collider.enabled = false;
            ChangeLevelScreen();
        }

        if (Change == 2)
        {
            _collider.enabled = true;
        }
    }


    void OnMouseDown()
    {
        Debug.Log(_level);
    }
}
