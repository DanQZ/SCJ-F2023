using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left")) {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("right")) {
            transform.position += Vector3.right * speed* Time.deltaTime;
        }
    }
}
