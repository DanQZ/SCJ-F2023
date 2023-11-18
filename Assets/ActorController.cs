using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public float moveDuration = 5;
    public Vector2 target = new Vector2(0, 5);
    public Vector2 target2 = new Vector2(0, 0);
    public Vector2 target3 = new Vector2(0, 0);
    public Vector2 target4 = new Vector2(0, 0);
    public Vector2 target5 = new Vector2(0, 0);

    void Start() 
    {
        StartCoroutine(MoveActor(target));
    }
    IEnumerator MoveActor(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float timeElapsed = 0;

        while (timeElapsed < moveDuration) 
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
