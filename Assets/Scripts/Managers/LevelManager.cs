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
        Instance = this;
        levelAudio = GetComponent<LevelAudio>(); 
    }

    private void Start()
    {
        levelAudio.PlaySceneMusic();
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
        if (PlayerPrefs.HasKey("Level")) {
            int sceneIndex = PlayerPrefs.GetInt("Level");
            if (sceneIndex > 1) {
                SceneManager.sceneLoaded += SceneManager_MainMenuLoaded;
                
            }
        }
        
    }

    private void SceneManager_MainMenuLoaded(Scene arg0, LoadSceneMode arg1)
    {
        int sceneIndex = PlayerPrefs.GetInt("Level");
        SpawnContinueButton.Instance.ContinueButton();
        SceneManager.sceneLoaded -= SceneManager_MainMenuLoaded;
    }

    public void ReloadThisScene()
    { 
        levelAudio?.PlayFailSound();
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);

    }
}