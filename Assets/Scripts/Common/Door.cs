using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstacleBehaviour
{
    public Collider2D doorCollider;
    public Animator doorAnimator;  
    public float openDelay = 0.5f; 

    private bool IsOpen = false; 

    public override void DisableObstacle()
    {
        if (!IsOpen)
        {
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    public override void EnableObstacle()
    { 
        IsOpen = false;
        doorCollider.enabled = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetBool(nameof(IsOpen), false);
        } 
    }
     
    void Start()
    {
        if (doorCollider == null)
        {
            doorCollider = GetComponent<Collider2D>();
        }
         
    }

    //wait for the animation to open
    private IEnumerator OpenDoorWithDelay()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool(nameof(IsOpen), true);
        }

        yield return new WaitForSeconds(openDelay);

        IsOpen = true;
        doorCollider.enabled = false;
    }
}
