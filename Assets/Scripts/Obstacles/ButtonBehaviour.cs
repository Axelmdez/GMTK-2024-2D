using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [Tooltip("This could only be one of: Obstacle door, elevator, rotating platform, wind obstacle")]
    public ObstacleBehaviour obstacleLinked;
    [Tooltip("Switch on mode: only and always turn the obstacle effects on, can also use switch off mode together")]
    public bool ObstacleTriggerButton;
    [Tooltip("Switch off mode: only and always turn the obstacle effects off, can also use switch on mode together")]
    public bool ObstacleUntriggerButton;
    [Tooltip("If this is checked, other two booleans cannot be checked!!! this button mode need to be held(large box should stay on top) to trigger obstacle")]
    public bool ElevatorButton;
    float buttonSizeY;
    Vector3 buttonUpPos;
    Vector3 buttonDownPos;
    float buttonSpeed = 1f;
    float buttonDelay = .2f;
    bool isPressed = false;
    
    void Awake()
    {
        buttonSizeY = transform.localScale.y;
        buttonUpPos = transform.position;
        buttonDownPos = new Vector3(transform.position.x, transform.position.y - buttonSizeY, transform.position.z);
    }

    void Update()
    {
        if (isPressed)
        {
            MoveButtonDown();
        }
        else
        {
            MoveButtonUp();
        }
    }

    void MoveButtonDown()
    {
        if (transform.position.y > buttonDownPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, buttonDownPos, buttonSpeed * Time.deltaTime);
        }
        else {
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    void MoveButtonUp()
    {
        if (transform.position.y < buttonUpPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, buttonUpPos, buttonSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = buttonUpPos;
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            isPressed = !isPressed;

            if (ObstacleTriggerButton && !obstacleLinked.isTriggered)
            {
                obstacleLinked.isTriggered = !obstacleLinked.isTriggered;
            }
            else if (ObstacleUntriggerButton && obstacleLinked.isTriggered)
            {
                obstacleLinked.isTriggered = !obstacleLinked.isTriggered;
            }
            else if (ElevatorButton) {
                obstacleLinked.isTriggered = true;
            }
        }
    }

    IEnumerator ButtonUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        if (ElevatorButton) {
            obstacleLinked.isTriggered = false;
            isPressed = false;
        }
        if (ObstacleTriggerButton && ObstacleUntriggerButton) {
            isPressed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            StartCoroutine(ButtonUpDelay(buttonDelay));
        }
    }

}