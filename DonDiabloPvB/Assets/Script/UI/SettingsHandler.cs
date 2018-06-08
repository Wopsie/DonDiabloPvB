using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Settings()
    {
        SetButtonActive(false);
        NewPlayerMovement.Instance.Velocity("Off");
        audioSource.Pause();
        SettingUI(true);
        ShaderController.Instance.TriggerEffect(2); 
    }

    public void Resume()
    {
        SettingUI(false);
        SetButtonActive(true);
        StartCoroutine(WaitTillBegin());
    }

    public void BackToMenu()
    {
        LevelManager.Instance.RemoveLevel();
        //Player to origin
        //Menu UI on
        //Playermovement off
        //Score to 0
    }

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

    IEnumerator WaitTillBegin()
    {
        ShaderController.Instance.TriggerEffect(1);
        yield return new WaitForSeconds(2);
        SetButtonActive(true);
        audioSource.Play();
        NewPlayerMovement.Instance.Velocity("On");
    }
}