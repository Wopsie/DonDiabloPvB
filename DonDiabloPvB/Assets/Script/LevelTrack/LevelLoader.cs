using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private LevelData level;

    private void Awake(){
        PlaceLevel("LevelTwo");
    }

    void PlaceLevel(string levelName){
        LevelData level = Resources.Load<LevelData>(levelName);
        Instantiate(level.levelObject);
        MeshGenerator generator = level.levelObject.GetComponentInChildren<MeshGenerator>();
        generator.meshPoints = level.points;
        generator.meshWidth = level.roadWidth;
        generator.texTiling = level.textureTiling;
        generator.pointsSpacing = level.pointSpacing;
        generator.SetRoadAfterSave();
    }
}
