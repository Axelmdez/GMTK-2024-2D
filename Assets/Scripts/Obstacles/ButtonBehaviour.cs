using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public DoorBehaviour door;
    public bool isDoorOpenButton;
    public bool isDoorCloseButton;
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
            // 设置物体为Kinematic
            

            isPressed = !isPressed;

            if (isDoorOpenButton && !door.isDoorOpen)
            {
                door.isDoorOpen = !door.isDoorOpen;
            }
            else if (isDoorCloseButton && door.isDoorOpen)
            {
                door.isDoorOpen = !door.isDoorOpen;
            }
        }
    }

    IEnumerator ButtonUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPressed = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            StartCoroutine(ButtonUpDelay(buttonDelay));
        }
    }
}