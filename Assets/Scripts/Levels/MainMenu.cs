using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    /// <summary>
    /// for some reason unity inspector wasn't letting me hook these darn buttons up 
    /// so I'm manually setting this. Feel free to scrap this entire script later 
    /// </summary> 

    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button quitButton; 

    void Start()
    { 
        startButton.onClick.AddListener(OnStartButtonPressed);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    public void OnStartButtonPressed()
    {
        LevelManager.Instance.StartGame();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
