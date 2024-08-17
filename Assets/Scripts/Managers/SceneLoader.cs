using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

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

    private void OnEnable()
    {
        LevelManager.OnGoalReached += LoadNextLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnGoalReached -= LoadNextLevel;
    }

    public void LoadLevelByIndex(int index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Roll Credits!"); 
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadLevelByIndex(currentSceneIndex + 1);
    }
}

