using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isDoorOpen = false;
    Vector3 doorClosePos;
    Vector3 doorOpenPos;
    float doorSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        doorClosePos = transform.position;
        doorOpenPos = new Vector3(transform.position.x,transform.position.y + 3f,transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDoorOpen)
        {
            OpenDoor();
        }
        else if (!isDoorOpen) {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (transform.position != doorOpenPos) {
            transform.position = Vector3.MoveTowards(transform.position,doorOpenPos,doorSpeed* Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if (transform.position != doorClosePos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorClosePos, doorSpeed * Time.deltaTime);
        }
    }
}
