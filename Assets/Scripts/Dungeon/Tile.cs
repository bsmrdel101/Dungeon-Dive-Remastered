using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float G;
    public float H;
    public float F => G + H;
    public bool IsWalkable;


    private void Start()
    {
        IsWalkable = GetComponent<Surface>().IsWalkable;
    }
}
