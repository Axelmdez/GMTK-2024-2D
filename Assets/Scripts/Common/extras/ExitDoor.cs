using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public bool isDoorOpen = false;
    public GameObject exit;

    // Start is called before the first frame update
    private void Awake()
    {
        exit.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isDoorOpen)
        {
            EnableDoor();
        }
        else if (!isDoorOpen)
        {
            DisableDoor();
        }
    }

    void EnableDoor()
    {
        
        if (exit != null) {
            // play open door animation
            exit.SetActive(true);
        }
        
    }

    void DisableDoor()
    {   
        
        if (exit != null)
        {
            // play close door animation
            exit.SetActive(false);
        }
        
    }
}
