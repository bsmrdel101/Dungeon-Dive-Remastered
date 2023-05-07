using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Progression")]
    public int Floor = 1;
    public int Area = 1;

    [Header("Rooms")]
    public GameObject[] FloorOneStartingRooms;
    public GameObject[] FloorOneRooms;
    public GameObject[] FloorTwoStartingRooms;
    public GameObject[] FloorTwoRooms;
    public GameObject[] FloorThreeStartingRooms;
    public GameObject[] FloorThreeRooms;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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

    // Takes in tile coordinates
    // Returns the Tile component
    public Tile GetTileFromCoords(float x, float y)
    {
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            if (tile.transform.position.x == x && tile.transform.position.y == y)
                return tile;
        }
        return null;
    }
}
