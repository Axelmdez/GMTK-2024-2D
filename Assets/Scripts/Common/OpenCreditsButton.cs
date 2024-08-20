using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCreditsButton : MonoBehaviour
{
    public GameObject creditsPanel;

    public void OpenCreditsPanel()
    {
        transform.gameObject.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
