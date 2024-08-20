using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Awake()
    {
        PlayerInteraction.tryExit += TryEscape;
    }
    private bool isPlayerInRange = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void TryEscape()
    {
        if (isPlayerInRange)
        {
            LevelManager.Instance.GoalReached();
        }
    }

}
