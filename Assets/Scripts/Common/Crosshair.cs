using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crosshair : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Vector3[] offsets;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        var s = SceneManager.GetActiveScene();
        if(s != null && s.buildIndex == 0)
        {
            SwapCursorSprite(sprites[2]);
        }
        else
            SwapCursorSprite(sprites[0]);
    }

    void Update()
    { 
        Vector3 mousePosition = Input.mousePosition;
         
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); 
        mousePosition.z = 0;
        transform.position = mousePosition + activeOffset;
    }

    public Vector3 activeOffset = Vector3.zero;

    public void SwapCursorSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
        activeOffset = offsets[sprites.IndexOf(newSprite)];
    }
}
