using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : ObstacleBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    public float waitTime = 2f;
    public bool isWaiting = true;
    public Transform player;
    private bool movingToEnd;
    private bool isBoxLifted;

    [SerializeField] float platformOffset = 1.5f;
    private Vector3 savedStartPos, savedEndPos;

    private void Start()
    {
        savedStartPos = startPoint.position + Vector3.up * platformOffset;
        savedEndPos = endPoint.position + Vector3.up * platformOffset;

        transform.position = savedStartPos;
        movingToEnd = true;
    }

    private void Awake()
    {
        PlayerInteraction.boxLifted += ChangeIsBoxLifted; ;
    }

    private void ChangeIsBoxLifted()
    {
        isBoxLifted = true;
    }

    /*
    protected override void Update()
    {
        if (!isWaiting)
        {
            MovePlatform();
        }
    }
    */
    void MovePlatform()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, savedEndPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, savedEndPos) < 0.1f)
            {
                StartCoroutine(WaitAtPoint());
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, savedStartPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, savedStartPos) < 0.1f)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Throwable") || collision.CompareTag("Liftable")) {

            collision.transform.SetParent(this.transform);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            
            collision.transform.SetParent(null);
        }
        if (collision.CompareTag("Throwable") || collision.CompareTag("Liftable"))
        {
            Transform boxParent = collision.transform.parent;

            if (!isBoxLifted)
            {
                collision.transform.SetParent(null);
            }
            else { 
                collision.transform.SetParent(player);
            }
        }
    }

    private void Update()
    {
        if(!isWaiting)
        {
            MovePlatform();
        }
    }

    public override void EnableObstacle()
    {
        isWaiting = true;
    }

    public override void DisableObstacle()
    {
        isWaiting=false;
    }
}