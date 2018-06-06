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
    public List<GameObject> obj = new List<GameObject>();
    private NewPlayerMovement PlayerMove;
    private AudioSource audioSource;
    private LevelManager levelManager;

    private void Awake()
    {
        PlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerMovement>();
        GameObject[] x = GameObject.FindGameObjectsWithTag("UI");
        audioSource = Camera.main.GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelLoader").GetComponent<LevelManager>();

        for (int i = 0; i < x.Length; i++)
        {
            obj.Add(x[i]);
            if (x[i].tag == "UI")
            {
                x[i].SetActive(false);
            }
        }
    }

    public void Settings()
    {
        SetActiveObjects();
        audioSource.Pause();
        //shader.TriggerEffect(0);//Is Broken will be fixed 
    }

    public void SetActiveObjects()
    {
        PlayerMove.Velocity("Off");
        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].name == "Settings")
            {
                obj[i].SetActive(true);
            }
        }
    }

    public void Resume()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(false);
        }
        StartCoroutine(WaitTillBegin());
    }

    public void Retry()
    {
        levelManager.RemoveLevel();
        //levelManager.PlaceLevel("Level" + startButton._Level);
    }

    IEnumerator WaitTillBegin()
    {
        yield return new WaitForSeconds(2);
        audioSource.Play();
        PlayerMove.Velocity("On");
    }
}