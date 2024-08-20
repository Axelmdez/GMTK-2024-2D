using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;
    public float spawnHeight;

    private GameObject anotherBox;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Player"))
        {
            if (anotherBox != null) 
            {
                GameObject.Destroy(anotherBox);
                anotherBox = null;
            }

            Vector2 spawnPosition = transform.position + new Vector3(0, spawnHeight, 0);
            GameObject newObject= Instantiate(box, spawnPosition, Quaternion.identity);
            Box newBox= newObject.GetComponent<Box>();
            newBox.SetSpawner(this);
            

        }
    }

    public void SetAnotherBox(GameObject gameObject)
    {
        anotherBox=gameObject;

    }
}
