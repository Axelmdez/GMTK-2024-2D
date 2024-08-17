using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public BoxType boxType;
    public GameObject smallPrefab;
    public GameObject mediumPrefab;
    public GameObject largePrefab;

    public void BoxTransform(Transform hitTransform, bool isShrink)
    {
        Vector2 boxPosition = Vector2.zero;

        
        boxPosition = hitTransform.position;

        GameObject newChild = null;

        if (isShrink)
        {

            switch (boxType)
            {
                case BoxType.large:
                    newChild = Object.Instantiate(mediumPrefab);
                    boxType = BoxType.medium;
                    break;

                case BoxType.medium:
                    newChild = Object.Instantiate(smallPrefab);
                    boxType = BoxType.small;
                    break;

                case BoxType.small:
                    Debug.Log("Box is already small, cannot shrink further.");
                    return; 
            }
        }
        else
        {
            // �Ŵ�ģʽ
            switch (boxType)
            {
                case BoxType.small:
                    newChild = Object.Instantiate(mediumPrefab);
                    boxType = BoxType.medium;
                    break;

                case BoxType.medium:
                    newChild = Object.Instantiate(largePrefab);
                    boxType = BoxType.large;
                    break;

                case BoxType.large:
                    Debug.Log("Box is already large, cannot transform further.");
                    return; 
            }
        }

        if (newChild != null)
        {

            Object.Destroy(hitTransform.gameObject);
            newChild.transform.position = boxPosition;
        }
    }
}

    public enum BoxType
{
    small,
    medium,
    large,
}
