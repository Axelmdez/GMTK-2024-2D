using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBehaviour : MonoBehaviour
{
    internal bool isTriggered;

    public virtual void SetTrigger(bool triggerValue)
    {
        if (triggerValue)
        { 
            EnableObstacle();
        }
        else 
        {
            DisableObstacle();
        }
        isTriggered = triggerValue;
    }
    public abstract void EnableObstacle();

    public abstract void DisableObstacle();
}
