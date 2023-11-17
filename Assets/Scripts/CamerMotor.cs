using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
 
 public Transform lookAt; //the player
 public float boundX=0.15f;
 public float boundY=0.05f;

    private void Start()
    {
        lookAt=GameObject.Find("Player").transform;
    }
 public void LateUpdate()
 {
    Vector3 delta = Vector3.zero;

    float deltaX=lookAt.position.x - transform.position.x;
    //left or right
    //are we inside the bounds on the x axis
    if(deltaX>boundX || deltaX<-boundX)
    {
        //is middle of camera is smaller than player than
        //player is on right and camera focus is on the left
        if(transform.position.x < lookAt.position.x)
        {
            delta.x=deltaX-boundX;
            
        }
        else
        {
            delta.x=deltaX+boundX;
        }
    }

    float deltaY=lookAt.position.y - transform.position.y;
    //are we inside the bounds on the y axis
    if(deltaY>boundY || deltaY<-boundY)
    {
        //is middle of camera is smaller than player than
        //player is on up and camera focus is on the down
        if(transform.position.y < lookAt.position.y)
        {
            delta.y=deltaY-boundY;
            
        }
        else
        {
            delta.y=deltaY+boundY;
        }
    }
    transform.position+= new Vector3(delta.x,delta.y,0);

 }
    
}