using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public class UnityStringEvent : UnityEvent<string>
{
}

/// <summary>
/// Gewoon een voorbeeld van een startup window speciaal voor jou omdat je zo cool bent
/// Het maakt gebruik van scriptableobjects om data te saven en doet op het moment vrij weinig naast de flow beetje zetten
/// Je kan een dataobject kiezen of maken met een naam, nu aan jou de taak om dit te koppelen aan je editor (als je dat wil ofcourse)
/// Als je vragen heb weet je me te vinden
/// 
/// btw.. let niet teveel op de code dit was ff snel gedaan
/// </summary>
public class StartUpWindow : EditorWindow
{
    private static StartUpWindow window;

    private enum ScreenState {
        Init,
        Input,
    };

    private ScreenState screenState;
    private string dataName;
    public static UnityStringEvent CreateFileEvent;

    [MenuItem("Tool/LevelEditor")]
    public static void ShowWindow()
    {
        window = new StartUpWindow();
        window.minSize = new Vector2(300, 100);
        window.maxSize = window.minSize;
        window.ShowUtility();
    }

    private void OnGUI()
    {
        if(CreateFileEvent == null){
            CreateFileEvent = new UnityStringEvent();
        }

        switch (screenState)
        {
            case ScreenState.Init:
                DrawInit();
                break;
            case ScreenState.Input:
                DrawInput();
                break;
        }
    }

    private void DrawInit()
    {
        if (GUILayout.Button("Choose"))
        {
            LevelData data = null;
            string path = EditorUtility.OpenFilePanel("LevelData", "", "asset");

            if (!string.IsNullOrEmpty(path))
            {
                data = Resources.Load<LevelData>(Path.GetFileNameWithoutExtension(path));
                Debug.Log("Data found: " + data);

                window.Close();
            }

            // TODO: stuur het naar je path creator ding of idk... doe iets Jochem..
        }

        if (GUILayout.Button("New File"))
        {
            screenState = ScreenState.Input;
        }
    }

    private void DrawInput()
    {
        dataName = EditorGUILayout.TextField("Data Name", dataName);

        if (GUILayout.Button("Create"))
        {
            if(!string.IsNullOrEmpty(dataName))
            {
                ScriptableObjectsUtility.CreateAsset<LevelData>(dataName);
                Debug.Log("Data created: " + dataName);
                //send Unity event that sends out file name of level
                CreateFileEvent.Invoke(dataName);
                window.Close();
            }
            else
            {
                EditorUtility.DisplayDialog("Invalid input", "You must give it a name first!", "OK");
            }
        }
    }
}
