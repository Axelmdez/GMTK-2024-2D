using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static event Action OnGoalReached;

    private LevelAudio levelAudio;

    private void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
            levelAudio = GetComponent<LevelAudio>();
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoalReached()
    {
        levelAudio.PlayWinSound();
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

    public void ReloadThisScene()
    { 
        levelAudio?.PlayFailSound();
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(currentScene.name);
    }
}