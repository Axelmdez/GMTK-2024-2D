//scraped for simplicity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : ObstacleBehaviour
{
    public float rotationSpeed = 20f;

    public override void DisableObstacle()
    {
        
    }

    public override void EnableObstacle()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }


    
}
