using UnityEngine;
/// <summary>
/// This class is a simple button
/// </summary>
public class SelectButton : MonoBehaviour { 
    [SerializeField] private AudioClip AudioClip;
    [SerializeField] private int LevelNumber;


    //This function changes the music file and the number of the level upon a button press. it also calls the Shadercontroller to start its function.
    public void ButtonSelect()
    {
        //Changes the music that you hear in the menu.
        MainMenuHandler.Instance.SetAudioClip(AudioClip);
        ShaderController.Instance.TriggerEffect(3);
        MainMenuHandler.Instance.SetLevelNumber(LevelNumber);
        MainMenuHandler.Instance.UIStartButton.SetActive(true);
        LevelManager.Instance.PlaceLevel("Level" + LevelNumber); 
    }
}
