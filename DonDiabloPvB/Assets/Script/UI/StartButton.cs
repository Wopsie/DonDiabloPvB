using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject _parent; 
    private LevelManager loader;
    private int _level;

    void Awake()
    {
        loader = FindObjectOfType<LevelManager>();
    }

    public void ButtonSelect()
    {
        MainMenuHandler.Instance.AudioSource.Play();
        ShaderController.Instance.TriggerEffect(1);
        SettingsHandler.Instance.SetButtonActive(true);
        MainMenuHandler.Instance.SelectButtons.SetActive(false);
        MainMenuHandler.Instance.UIStartButton.SetActive(false);        
    }
}