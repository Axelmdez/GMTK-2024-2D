using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public Transform[] boxes;
    public Transform player;
    public void Restart()
    {
        SpawnManager.RequestSpawn(Spawnables.Box);
    }
}

