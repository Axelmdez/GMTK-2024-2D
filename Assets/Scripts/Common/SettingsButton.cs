using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject panel;
    public void LoadSettings()
    {
        panel.SetActive(true);
    }
    public void CloseSetting()
    {
        panel.SetActive(false);
    }
}
