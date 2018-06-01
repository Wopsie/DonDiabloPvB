using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {
    private GameObject settings;
    private SettingsHandler settingHandler;

    void Start()
    {
        settingHandler = GetComponentInParent<SettingsHandler>();
    }

    public void SettingButton()
    {
        settingHandler.Settings();
    }
}
