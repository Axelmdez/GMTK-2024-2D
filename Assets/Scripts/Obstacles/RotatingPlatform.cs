//scraped for simplicity

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingPlatform : ObstacleBehaviour
{
    public float rotationSpeed = 20f;

    public override void EnableObstacle()
    {
        isTriggered = false;   
    }

    public void Update()
    {
        if (isTriggered) RotatePlatform();
    }

    private void RotatePlatform()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }

    public override void DisableObstacle()
    {
        isTriggered = true;    
    } 
}
