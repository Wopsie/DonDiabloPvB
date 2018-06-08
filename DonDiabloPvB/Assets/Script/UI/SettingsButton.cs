using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

    public void SettingButton()
    {
        SettingsHandler.Instance.Settings();
    }

    public void BackToMenu()
    {
        SettingsHandler.Instance.BackToMenu();
    }

    public void Resume()
    {
        SettingsHandler.Instance.Resume();
    }
}
