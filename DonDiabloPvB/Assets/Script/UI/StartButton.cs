using UnityEngine;

/// <summary>
/// When song is set shader opens partly and then this script is used the Start button.
/// When pressed the shader opens further, song and game starts.
/// </summary>
public class StartButton : MonoBehaviour
{
    /// <summary>
    /// Play set audioclip,Opens Shader further,Sets all button off of the menu.
    /// </summary>
    public void ButtonSelect()
    {
        MainMenuHandler.Instance.AudioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        SettingsHandler.Instance.SetButtonActive(true);
        MainMenuHandler.Instance.SelectButtons.SetActive(false);
        MainMenuHandler.Instance.UIStartButton.SetActive(false);        
    }
}