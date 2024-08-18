using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    public float waitTime = 2f;

    private bool movingToEnd;
    private bool isWaiting;

    private void Start()
    {
        transform.position = startPoint.position;
        movingToEnd = true;
        isWaiting = false;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
            {
                StartCoroutine(WaitAtPoint());
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPoint.position) < 0.1f)
            {
                StartCoroutine(WaitAtPoint());
                movingToEnd = true;
            }
        }
    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}