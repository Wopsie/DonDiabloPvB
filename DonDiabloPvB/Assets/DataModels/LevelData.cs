using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class LevelData : ScriptableObject 
{
    public GameObject levelObject;
    public float roadWidth;
    public Vector2[] points;
    public float pointSpacing;
    public float textureTiling;
    //think of way to include obstacles
}
