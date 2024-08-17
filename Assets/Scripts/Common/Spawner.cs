using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{ 
    public Spawnables SpawnerID;
    public GameObject prefabToSpawn;

    private void OnEnable()
    {  
        SpawnManager.RegisterSpawner(SpawnerID, this);
    }

    private void OnDisable()
    {
        SpawnManager.UnregisterSpawner(SpawnerID);
    }

    // Method to spawn the prefab at the spawner's location
    public void Spawn()
    {
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }
    }
}
