using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneChecker
{
    static SceneChecker()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
    }

    private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        Debug.Log("Scene: " + scene.name);

        if(scene.name == "ToolScene")
        {
            EditorApplication.ExecuteMenuItem("Tool/JochemsKekkeOpstartScherm");
        }
    }
}
