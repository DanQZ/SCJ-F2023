using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5f;
    public Transform topLeft;
    public Transform topRight;
    public Transform botLeft;
    public Transform botRight;
    public SpriteRenderer POVSprite;
    public Camera mc;

    void Start(){
        mc.GetComponent<Camera>().orthographicSize = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }
    void Controls(){
        speed = 5f * transform.localScale.x;
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
            transform.localScale /= 1.01f;
            mc.GetComponent<Camera>().orthographicSize /= 1.01f;
        }
        if (Input.GetKey("down")) {
            transform.localScale *= 1.01f;
            mc.GetComponent<Camera>().orthographicSize *= 1.01f;
        }
    }
}
