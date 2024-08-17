using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTester : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnManager.RequestSpawn(Spawnables.Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnManager.RequestSpawn(Spawnables.Box);
        }
    }
}
