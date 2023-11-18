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
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changeInterval)
        {
            SetRandomDirection();
            timeSinceLastChange = 0f;
        }

        Move();
    }

    private void Move()
    {
        transform.position += (randomDirection * speed * Time.deltaTime);
    }

    private void SetRandomDirection()
    {
        float x = Random.Range(-16f, 16f);
        if(transform.position.x > 16f){
            x = -1f* Mathf.Abs(x);
        }
        if(transform.position.x < -16f){
            x = Mathf.Abs(x);
        }

        float y = Random.Range(-9, 9);
        if(transform.position.y > 9){
            y = -1f* Mathf.Abs(y);
        }
        if(transform.position.y < -9){
            y = Mathf.Abs(y);
        }
        
    
        randomDirection = new Vector3(x, y, 0f);
        speed = Random.Range(0.3f,1f);
        changeInterval = Random.Range(0.1f, 2f);
    }
}
