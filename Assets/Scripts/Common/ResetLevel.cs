using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    public void ResetRecords()
    {
        if (PlayerPrefs.HasKey("Level")) {
            PlayerPrefs.DeleteKey("Level");
            PlayerPrefs.Save();
        }
    }
}
