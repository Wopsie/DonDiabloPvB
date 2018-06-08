using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinners : MonoBehaviour
{
    private bool turn = true;
    public float speed = 0.1F;

    void Update()
    {
        if (turn == true)
        {
            transform.Rotate(Vector3.up * speed);
        }
    }


    public void Spin()
    {
        StartCoroutine(Startspin());
    }

    IEnumerator Startspin()
    {
        turn = true;
        yield return new WaitForSeconds(2);
        turn = false;
    }
}
