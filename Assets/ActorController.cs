using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{   
    public float speed;
    public float changeInterval; 

    private float timeSinceLastChange;
    private Vector3 randomDirection;

    private void Start()
    {   
        speed = Random.Range(1f,2f);
        changeInterval = Random.Range(0.5f, 3f);
        timeSinceLastChange = 0f;
        SetRandomDirection();
    }

    private void Update()
    {
        
    }
}
