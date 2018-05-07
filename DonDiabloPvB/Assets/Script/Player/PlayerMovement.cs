using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private PlayerInput input;
    private int LanesActive;
    private Dictionary<TrackSpawner, int> spawnerList = new Dictionary<TrackSpawner, int>();

    private bool moveLeft = true;
    private Vector3 leftPos = new Vector3(0, 0.75f, 0);
    private Vector3 rightPos = new Vector3(2.6f, 0.75f, 0);

    private void Start(){
        GameObject[]  spawners = GameObject.FindGameObjectsWithTag(Tags.SpawnerTag);
        for (int i = 0; i < spawners.Length; i++){

            if(spawners[i].gameObject.name == "TrackSpawner"){
                spawnerList.Add(spawners[i].GetComponent<TrackSpawner>(), 0);
            }else{
                spawnerList.Add(spawners[i].GetComponent<TrackSpawner>(), 1);
            }
        }

        input = GetComponent<PlayerInput>();
        input.OnPressButton += GetPress;
        input.OnHoldButton += GetHold;
        input.OnReleaseButton += GetRelease;
    }

    void GetPress(){
        Debug.Log("PRESS");
        //if no obstacle is in front
        //slerp to other lane

        if (moveLeft){
            MoveLane(false);
        }
        else
        {
            MoveLane(true);
        }
    }

    void GetHold(){
        //Debug.Log("HOLD");
    }

    void GetRelease(){
        //Debug.Log("RELEASE");
    }

    private void Update()
    {
        if (moveLeft){
            transform.position = Vector3.Slerp(transform.position, leftPos, 0.5f);
        }else{
            transform.position = Vector3.Slerp(transform.position, rightPos, 0.5f);
        }
    }

    void MoveLane(bool left){
        Debug.Log("MOVE LANE");
        
        //use raycast to check if the lane is where you are
        if (left){
            //raycast to the left
            RaycastHit hit;
            //raycast up from the position where the lane should be appearing
            if (Physics.Raycast(new Vector3(0,-1,1), transform.TransformDirection(Vector3.up), out hit, 2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

                Debug.Log("MOVE TO THE LEFT");
                moveLeft = true;
            }
            else
            {
                Debug.Log("CANNOT");
            }
        }else{
            RaycastHit hit;
            //raycast up from the position where the lane should be appearing
            if (Physics.Raycast(new Vector3(2.6f, -1, 1), transform.TransformDirection(Vector3.up), out hit, 2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

                Debug.Log("MOVE TO THE RIGHT");
                moveLeft = false;
            }
            else
            {
                Debug.Log("CANNOT");
            }
        }
    }
}
