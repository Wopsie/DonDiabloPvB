using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

    private GameObject settings;

    private void OnMouseDown()
    {
        Time.timeScale = 0;
        //settings.SetActive(true);
    }
}
