using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Holds all data of the current game/level.
/// </summary>
[CreateAssetMenu]
public class LevelData : ScriptableObject{
    public GameObject levelTrackObj;
    public GameObject levelBackgroundObj;
    public float roadWidth;
    public Vector2[] points;
    public float pointSpacing;
    public float textureTiling;
}