using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBehaviour : MonoBehaviour
{
    public bool isTriggered = false;

    // Start is called before the first frame update

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isTriggered)
        {
            EnableObstacle();
        }
        else if (!isTriggered) {
            DisableObstacle();
        }
    }

    public abstract void EnableObstacle();

    public abstract void DisableObstacle();
}
