using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour {
    [SerializeField]
    public List<GameObject> obj = new List<GameObject>();

    public void Settings()
    {
        SetActiveObjects();
        //shader.TriggerEffect(0);//Is Broken will be fixed 
    }

    public void SetActiveObjects()
    {
        for (int i = 0; i < obj.Count; i++)
        {   if (obj[i].activeSelf == true)
            {
                obj[i].SetActive(false);
            }
            else if (obj[i].activeSelf == false)
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
    }

    public void Retry()
    {
        //Restart Scene?!?!
    }
}
