using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    private Vector3 position;

    public Vector3 GetPosition()
    {
        position = gameObject.transform.position;
        return position;
    }
 
   
}
