using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCreditsButton : MonoBehaviour
{
    public GameObject settingPanel;

    public void CloseCreditsPanel()
    {   
        transform.gameObject.SetActive(false);
        settingPanel.SetActive(true);
    }
}
