using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5f;
    [SerializeField] Transform topLeft;
    [SerializeField] Transform topRight;
    [SerializeField] Transform botLeft;
    [SerializeField] Transform botRight;

    // Update is called once per frame
    void Update()
    {
        Controls();
    }
    void Controls(){
        if (Input.GetKey("w")) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            transform.position -= Vector3.right * speed* Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            transform.position -= Vector3.up * speed* Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            transform.position += Vector3.right * speed* Time.deltaTime;
        }
        
        if (Input.GetKey("up")) {
            transform.position += Vector3.up * speed* Time.deltaTime;
        }
        if (Input.GetKey("down")) {
            transform.position -= Vector3.up * speed* Time.deltaTime;
        }
    }
}
