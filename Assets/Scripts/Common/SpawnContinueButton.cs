using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnContinueButton : MonoBehaviour
{   
    // Drag this into Canvas in main scene!
    public GameObject continueButton;
    public static SpawnContinueButton Instance
    {
        get; private set;
    }
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("Level")) {
            ContinueButton();
        }
    }
    public void ContinueButton()
    {
        //SceneLoader.Instance.LoadLevelByIndex(index);
        continueButton.SetActive(true);
    }
}
