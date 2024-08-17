using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static event Action OnGoalReached;

    private void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoalReached()
    {
        OnGoalReached?.Invoke();
    } 

    public void StartGame()
    { 
        SceneLoader.Instance.LoadLevelByIndex(1); 
    }

    public void LoadMainMenu()
    {
        SceneLoader.Instance.LoadLevelByIndex(0);  
    }
}