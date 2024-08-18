using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private HashSet<Collider2D> collidingBoxes = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.Instance.ReloadThisScene();
        }
        else if (collision.CompareTag("Throwable") || collision.CompareTag("Liftable"))
        {
            Destroy(collision.gameObject);
        }
        /*else if (collision.CompareTag("Pushable"))
        {
            collidingBoxes.Add(collision);
        }*/
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            collidingBoxes.Remove(collision);
        }
    }

    private void Update()
    {
        List<Collider2D> toRemove = new List<Collider2D>();

        foreach (var box in collidingBoxes)
        {
            if (box != null && box.bounds.min.y <= transform.position.y - transform.localScale.y / 2)
            {
                Rigidbody2D rb = box.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                // Calculate the bottom position of the lava collider
                Vector2 bottomPosition = new Vector2(box.transform.position.x, transform.position.y - (transform.localScale.y / 2));
                box.transform.position = bottomPosition;
                toRemove.Add(box);
            }
        }

        foreach (var box in toRemove)
        {
            collidingBoxes.Remove(box);
        }
    }*/
}