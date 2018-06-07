using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{

    public static SettingsHandler Instance { get { return GetInstance(); } }

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
    [SerializeField]
    public List<GameObject> SettingObjects = new List<GameObject>();
    private NewPlayerMovement PlayerMove;
    private AudioSource audioSource;
    private LevelManager levelManager;

    private void Awake()
    {
        PlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMovement>();
        SettingObjects.AddRange(GameObject.FindGameObjectsWithTag("SettingUI"));
        SettingObjects.AddRange(GameObject.FindGameObjectsWithTag("SettingButton"));
        audioSource = Camera.main.GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelLoader").GetComponent<LevelManager>();
        for (int i = 0; i < SettingObjects.Count; i++)
        {
            SettingObjects[i].SetActive(false);
        }
    }

    public void Settings()
    {
        SetButtonActive(false);
        PlayerMove.Velocity("Off");
        audioSource.Pause();
        SettingUI(true);
        ShaderController.Instance.TriggerEffect(0); 
    }

    public void Resume()
    {
        Debug.Log("Hi");
        SettingUI(false);
        SetButtonActive(true);
        StartCoroutine(WaitTillBegin());
    }

    public void Retry()
    {
        levelManager.RemoveLevel();
        //levelManager.PlaceLevel("Level" + startButton._Level);
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
        yield return new WaitForSeconds(2);
        SetButtonActive(true);
        audioSource.Play();
        PlayerMove.Velocity("On");
    }
}