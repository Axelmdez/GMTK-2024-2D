using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public void ContinueLevel()
    {
        if (PlayerPrefs.HasKey("Level")) {
            int index = PlayerPrefs.GetInt("Level");
            SceneLoader.Instance.LoadLevelByIndex(index);
        }
       
    }
}
