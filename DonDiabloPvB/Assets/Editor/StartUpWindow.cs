using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public class UnityStringEvent : UnityEvent<string> { }

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
    public static void ShowWindow(){
        window = new StartUpWindow();
        window.minSize = new Vector2(300, 100);
        window.maxSize = window.minSize;
        window.ShowUtility();
    }

    private void OnGUI(){
        if(CreateFileEvent == null){
            CreateFileEvent = new UnityStringEvent();
        }

        switch (screenState){
            case ScreenState.Init:
                DrawInit();
                break;
            case ScreenState.Input:
                DrawInput();
                break;
        }
    }

    private void DrawInit(){
        if (GUILayout.Button("New File")){
            screenState = ScreenState.Input;
        }
    }

    private void DrawInput(){
        dataName = EditorGUILayout.TextField("Data Name", dataName);

        if (GUILayout.Button("Create")){
            if(!string.IsNullOrEmpty(dataName)){
                ScriptableObjectsUtility.CreateAsset<LevelData>(dataName);
                Debug.Log("Data created: " + dataName);
                //send Unity event that sends out file name of level
                if (CreateFileEvent != null)
                    CreateFileEvent.Invoke(dataName);
                else
                    Debug.LogError("NOTHING IS LISTENING TO THE EVENT");
                window.Close();
            }else{
                EditorUtility.DisplayDialog("Invalid input", "You must give it a name first!", "OK");
            }
        }
    }
}
