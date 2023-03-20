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
        if (Mathf.Abs(pos2.position.x - pos1.position.x) == Mathf.Abs(pos2.position.y - pos1.position.y))
            return (int)Mathf.Abs(pos2.position.x - pos1.position.x);
        else
            return (int)(Mathf.Abs(Mathf.FloorToInt(Mathf.Sqrt((Mathf.Pow(pos2.position.x - pos1.position.x, 2)) + (Mathf.Pow(pos2.position.y - pos1.position.y, 2))))));
    }
}
