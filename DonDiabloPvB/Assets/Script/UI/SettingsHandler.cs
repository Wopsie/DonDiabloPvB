using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// SettingsHandler handles all functions the settings/pause menu uses. 
    /// </summary>

public class SettingsHandler : MonoBehaviour
{
    #region Singleton
    private static SettingsHandler instance;

    private static SettingsHandler GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SettingsHandler>();
        }

        return instance;
    }
    #endregion
    public static SettingsHandler Instance { get { return GetInstance(); } }
    [SerializeField]
    public List<GameObject> SettingObjects = new List<GameObject>();
    private AudioSource audioSource;


    /// <summary>
    /// The Awake function adds all objects with the tag "SettingUI" and "SettingButton" to a list, and it gets the audiosource of the camera.
    /// UI is set inactive at begin of the game because we wont start in pause menu.
    /// </summary>
    private void Awake()
    {
        SettingObjects.AddRange(GameObject.FindGameObjectsWithTag("SettingUI"));
        SettingObjects.AddRange(GameObject.FindGameObjectsWithTag("SettingButton"));
        audioSource = Camera.main.GetComponent<AudioSource>();
        for (int i = 0; i < SettingObjects.Count; i++)
        {
            SettingObjects[i].SetActive(false);
        }
    }

    /// <summary>
    /// When activate turn off the settings button and activate the UI of the settings, pauses the music and movement.
    /// </summary>
    public void Settings()
    {
        SetButtonActive(false);
        //NewPlayerMovement.Instance.Velocity("Off");}
        audioSource.Pause();
        SettingUI(true);
        ShaderController.Instance.TriggerEffect(2); 
    }

    /// <summary>
    /// Same as Setting() but the opposite and turns button back on and inactivate UI waits for shader and start movement again.
    /// </summary>
    public void Resume()
    {
        SettingUI(false);
        SetButtonActive(true);
        StartCoroutine(WaitTillBegin());
    }

    /// <summary>
    /// Function in the pause menu for when you want to go back to the main menu to choose a new song or to stop the game.
    /// Sets UI off and everything of the main menu UI on resets map and score.
    /// </summary>
    public void BackToMenu()
    {
        SettingUI(false);
        SetButtonActive(true);
        MainMenuHandler.Instance.SelectButtons.SetActive(true);
        NewPlayerMovement.Instance.Reset();
        LevelManager.Instance.RemoveLevel();
        ObstacleHelper.Instance.CurrentScore = 0;
    }

    /// <summary>
    /// With given bool turns on/off SettingButton.
    /// button is to activate the setting UI.
    /// </summary>
    /// <param name="a"></param>
    public void SetButtonActive(bool a)
    {
        for (int i = 0; i < SettingObjects.Count; i++)
        {
            if (SettingObjects[i].tag == "SettingButton")
            {
                SettingObjects[i].SetActive(a);
            }
        }
    }

    /// <summary>
    /// With given bool turns on/off SettingUI
    /// SettingUI is to choose to resume or back to menu.
    /// </summary>
    /// <param name="a"></param>
    public void SettingUI(bool a)
    {
        for (int i = 0; i < SettingObjects.Count; i++)
        {
            if (SettingObjects[i].tag == "SettingUI")
            {
                SettingObjects[i].SetActive(a);
            }
        }
    }


    /// <summary>
    /// Waits for the shader to open again before starting audio and movement.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitTillBegin()
    {
        ShaderController.Instance.TriggerEffect(1);
        yield return new WaitForSeconds(2);
        SetButtonActive(true);
        audioSource.Play();
        NewPlayerMovement.Instance.Velocity("On");
    }
}