using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

[System.Serializable]
public class UnityStringEvent : UnityEvent<string> { }

/// <summary>
/// Editor tool for level editing.
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
