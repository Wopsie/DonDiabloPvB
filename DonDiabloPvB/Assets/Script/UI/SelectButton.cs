using UnityEngine;

/// <summary>
/// Each Mainmenu button for choosing the song uses this script.
/// It sets Audioclip and level number and makes the Menu go to the next step.
/// </summary>
public class SelectButton : MonoBehaviour { 
    [SerializeField] private AudioClip AudioClip;
    [SerializeField] private int LevelNumber;

    /// <summary>
    /// Takes care when MainMenu button is clicked, sets song opens partly the windscreen and activate ready button.
    /// </summary>
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
