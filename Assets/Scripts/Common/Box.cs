using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public BoxType boxType;
}

public enum BoxType
{
    small,
    medium,
    large,
}
