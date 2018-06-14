using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which only spins the spinner of the DJ Booth.
/// </summary>
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
