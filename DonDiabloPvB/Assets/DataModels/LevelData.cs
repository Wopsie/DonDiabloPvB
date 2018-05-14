using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class LevelData : ScriptableObject 
{
    public string LevelName;
    public List<Vector2> Points = new List<Vector2>();
    public AudioClip LevelMusic;
}
