using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeon")]
    [SerializeField] private GameObject[] _roomPrefabList;


    private void Start()
    {
        
    }

    // Takes in two tile positions
    // Returns the distance between the tiles
    public int DistanceBetweenTwoPoints(Transform pos1, Transform pos2)
    {
        // calculate the distance between two tiles based on their transform coordinates
        int xDiff = (int)Mathf.Abs(pos1.position.x - pos2.position.x);
        int yDiff = (int)Mathf.Abs(pos1.position.y - pos2.position.y);

        // the distance between two tiles is the maximum difference between their x and y coordinates
        return Mathf.Max(xDiff, yDiff);
    }
}
