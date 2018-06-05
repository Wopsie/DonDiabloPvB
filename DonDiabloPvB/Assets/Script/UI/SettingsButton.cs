using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {
    private GameObject settings;
    private SettingsHandler settingHandler;

    void Start()
    {
        settingHandler = GameObject.FindGameObjectWithTag("SettingHandler").GetComponent<SettingsHandler>();
    }

    public void SettingButton()
    {
        settingHandler.Settings();
    }

    public void Retry()
    {
        settingHandler.Retry();
    }

    public void Resume()
    {
        settingHandler.Resume();
    }
}
