using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private int _level;

    public void ButtonSelect()
    {
        MainMenuHandler.Instance.AudioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        SettingsHandler.Instance.SetButtonActive(true);
        MainMenuHandler.Instance.SelectButtons.SetActive(false);
        MainMenuHandler.Instance.UIStartButton.SetActive(false);        
    }
}