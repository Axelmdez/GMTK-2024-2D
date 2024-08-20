using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstacleBehaviour
{
    public Collider2D doorCollider;
    public Animator doorAnimator;  
    public float openDelay = 0.5f; 

    private bool isOpen = false; 

    public override void DisableObstacle()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    public override void EnableObstacle()
    {
        if (isOpen)
        {
            isOpen = false;
            doorCollider.enabled = true;

            if (doorAnimator != null)
            {
                doorAnimator.SetBool(nameof(isOpen), false);
            }
        }
    }
     
    void Start()
    {
        if (doorCollider == null)
        {
            doorCollider = GetComponent<Collider2D>();
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool(nameof(isOpen), false);
        }
    }

    //wait for the animation to open
    private IEnumerator OpenDoorWithDelay()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool(nameof(isOpen), true);
        }

        yield return new WaitForSeconds(openDelay);

        isOpen = true;
        doorCollider.enabled = false;
    }
}
