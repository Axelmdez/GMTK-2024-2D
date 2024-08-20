using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class Box : MonoBehaviour
{
    public BoxType boxType;
    public GameObject smallPrefab;
    public GameObject mediumPrefab;
    public GameObject largePrefab;

    private BoxAudio boxAudio;
    private Rigidbody2D rb;

    private BoxSpawner spawner;

    private void Start()
    {
        boxAudio = GetComponent<BoxAudio>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WindZone"))
        {
            AreaEffector2D areaEffector = other.GetComponent<AreaEffector2D>();

            if (areaEffector != null)
            {
                switch (boxType)
                {
                    case BoxType.small:
                        areaEffector.enabled = true;
                        break;
                    case BoxType.medium:
                        areaEffector.enabled = true;
                        break;
                    case BoxType.large:
                        areaEffector.enabled = false;
                        break;
                }
            }
        }
    }

    public void BoxTransform(Transform hitTransform, bool isShrink)
    {
        Vector2 boxPosition = hitTransform.position;
        GameObject newChild = null;

        if (isShrink)
        {
            boxAudio.PlayScalingSoundDown(); // 0 for shrink
            switch (boxType)
            {
                case BoxType.large:
                    newChild = Instantiate(mediumPrefab);
                    boxType = BoxType.medium;
                    break;
                case BoxType.medium:
                    newChild = Instantiate(smallPrefab);
                    boxType = BoxType.small;
                    break;
                case BoxType.small:
                    Debug.Log("Box is already small, cannot shrink further.");
                    break;
            }
        }
        else
        {
            boxAudio.PlayScalingSoundUp(); // 1 for growth
            switch (boxType)
            {
                case BoxType.small:
                    newChild = Instantiate(mediumPrefab);
                    boxType = BoxType.medium;
                    break;
                case BoxType.medium:
                    newChild = Instantiate(largePrefab);
                    boxType = BoxType.large;
                    break;
                case BoxType.large:
                    Debug.Log("Box is already large, cannot transform further.");
                    return;
            }
        }

        if (newChild != null)
        {
            if (spawner != null)
            {
                Box newBox = newChild.GetComponent<Box>();
                newBox.SetSpawner(spawner);

            }

            Destroy(hitTransform.gameObject);
            newChild.transform.position = boxPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        boxAudio.PlayHitSound();
    }

    public void SetSpawner(BoxSpawner boxSpawner)
    {
        spawner= boxSpawner;
        spawner.SetAnotherBox(gameObject);
    }
}

public enum BoxType
{
    small,
    medium,
    large,
}
