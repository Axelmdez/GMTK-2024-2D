using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything we want to spawn we have to add to this list, make a prefab and thats it!
/// </summary>
public enum Spawnables
{
    Player,
    Box
}

public class SpawnManager 
{  
    private static Dictionary<Spawnables, Spawner> spawners = new Dictionary<Spawnables, Spawner>();

 
    public static void RegisterSpawner(Spawnables id, Spawner spawner)
    {
        if (!spawners.ContainsKey(id))
        {
            spawners.Add(id, spawner);
        }
    } 
    public static void UnregisterSpawner(Spawnables id)
    {
        if (spawners.ContainsKey(id))
        {
            spawners.Remove(id);
        }
    } 

    public static void RequestSpawn(Spawnables id)
    {
        if (spawners.ContainsKey(id))
        {
            spawners[id].Spawn();
        }
    }
}
