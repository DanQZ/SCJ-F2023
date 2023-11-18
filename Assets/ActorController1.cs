using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public float moveDuration = 2;
    public Vector2 target = new Vector2(0, 50);

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
