using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDamagable
{
    //a bool to check if the player is in the window to act
    private bool InWindow;
    private enum MyEnum {ok, good, perfect };
    private float distanceToBeDestroyed;

    private void Start()
    {
        SetInputWindow(2);

    }

    private void Update()
    {
        //CheckIfDestroyable();

    }


    public float CheckPlayerDistance()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag(Tags.PlayerTag).transform;
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log(dist + " distance");
        return dist;
    }

    /// <summary>
    /// what to do when a player input is recieved
    /// </summary>
    public void OnPlayerInput()
    {
        //play a animation or something to show the obstacle got beat
        // for now just detroy it 
        //Destroy(gameObject);
        if (CheckIfDestroyable())
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerContact()
    {
        //use the function on te player character to indicate you got hit
    }

    /// <summary>
    /// this function determs how far away the player has to push the button 
    /// </summary>
    /// <param name="distance"> minimun distance, from here the rest of the distance is calculated </param>
    public void SetInputWindow(float dist)
    {
        //this should be changed to allow several levels of player input perfection
        distanceToBeDestroyed = dist;

    }
    private bool CheckIfDestroyable()
    {
        if (CheckPlayerDistance() <= distanceToBeDestroyed)
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}
