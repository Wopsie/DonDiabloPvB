using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    private float _level;


    void OnMouseDown()
    {
        Debug.Log(_level);
    }

    public void LevelNumber(float levelNumber)
    {
        _level = levelNumber;
    }
}
