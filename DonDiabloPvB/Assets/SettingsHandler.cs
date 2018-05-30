using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour {
    [SerializeField]
    public List<GameObject> obj = new List<GameObject>();
    public ShaderController shader;

    public void Settings()
    {
        SetactiveObjects();
        shader.TriggerEffect();//Is Broken will be fixed 
    }

    public void SetactiveObjects()
    {
        Debug.Log("Hi");
        
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
}
