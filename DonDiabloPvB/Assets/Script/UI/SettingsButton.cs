using UnityEngine;


/// <summary>
/// Class only used for setting buttons to call SettingsHandler functions.
/// </summary>
public class SettingsButton : MonoBehaviour {

    /// <summary>
    /// When setting/pause button is pressed called function to activate UI and his buttons for Resume() and BackToMenu.
    /// </summary>
    public void SettingButton()
    {
        SettingsHandler.Instance.Settings();
    }

    /// <summary>
    /// Calls BackToMenu() of SettingsHandler.
    /// </summary>
    public void BackToMenu()
    {
        SettingsHandler.Instance.BackToMenu();
    }
    
    /// <summary>
    /// Calls Resume() of SettingsHandler
    /// </summary>
    public void Resume()
    {
        SettingsHandler.Instance.Resume();
    }
}
