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
    public GameManager gm;

    void Start(){
        mc.GetComponent<Camera>().orthographicSize = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }
    float zoomSpeed = 1.005f;
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
            transform.localScale /= zoomSpeed;
            gm.SetActorSize(transform.localScale * 0.5f);
            mc.GetComponent<Camera>().orthographicSize /= zoomSpeed;
        }
        if (Input.GetKey("down")) {
            transform.localScale *= zoomSpeed;
            gm.SetActorSize(transform.localScale* 0.5f);
            mc.GetComponent<Camera>().orthographicSize *= zoomSpeed;
        }
    }
}
