using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDoor : ObstacleBehaviour
{
    Vector3 doorClosePos;
    Vector3 doorOpenPos;
    float doorSpeed = 10f;

    Transform doorSpriteTransform;

    private void Awake()
    {
        doorClosePos = transform.position;
        doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }
    public override void EnableObstacle()
    {
        if (transform.position != doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorOpenPos, doorSpeed * Time.deltaTime);
        }
    }

    public override void DisableObstacle()
    {
        if (transform.position != doorClosePos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorClosePos, doorSpeed * Time.deltaTime);
        }
    }
}
