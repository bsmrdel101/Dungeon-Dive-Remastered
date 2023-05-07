using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [Header("Tile Lists")]
    private List<Tile> _currentNodes = new List<Tile>();
    private List<Tile> _processedNodes = new List<Tile>();

    [Header("References")]
    [SerializeField] private DungeonManager _dungeonManager;

    
    // Returns a list of all the nodes that should be moved to, in order to reach the target
    public List<Tile> GetTargetPath(Tile startPos, Tile target)
    {
        bool reachedTarget = false;
        List<Tile> finalPath = new List<Tile>();
        Tile currentPos = startPos;

        // Determine processed nodes
        while (!reachedTarget)
        {
            SetNodePoints(currentPos, target);
            Tile nextTile = GetLowestCostNode(currentPos);
            _processedNodes.Add(nextTile);
            currentPos = nextTile;
            if (currentPos.name == target.name) reachedTarget = true;
        }
        finalPath = _processedNodes;

        // TEMP: Removes color highlight from all tiles
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            if (GameObject.Find(tile.name).GetComponent<SpriteRenderer>())
                GameObject.Find(tile.name).GetComponent<SpriteRenderer>().color = new Color(0.3490566f, 0.3490566f, 0.3490566f, 1f);
        }
        // TEMP: Adds color highlight
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            if (finalPath.Contains(tile))
            {
                GameObject.Find(tile.name).GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 1f, 1f);
            }
        }

        return finalPath;
    }

    // Get the lowest F cost tile in the available path
    private Tile GetLowestCostNode(Tile currentPos)
    {
        GameObject newObj = new GameObject();
        Tile newTile = newObj.AddComponent<Tile>();
        newTile.G = 999;
        newTile.H = 999;
        Destroy(newObj);

        Tile lowestCostTile = newTile;
        foreach (Tile tile in _currentNodes)
        {
            if (_dungeonManager.DistanceBetweenTwoPoints(tile.transform, currentPos.transform) == 1 && tile.F < lowestCostTile.F && tile.IsWalkable)
            {
                lowestCostTile = tile;
            }
        }
        return lowestCostTile;
    }

    // Set the G and H cost values for each tile on the board
    private void SetNodePoints(Tile startPos, Tile target)
    {
        List<Tile> tilesList = CloneCurrentTilesArray();
        foreach (Tile tile in tilesList)
        {
            if (tile != startPos)
            {
                // Calculate the base G and H cost values
                tile.G = _dungeonManager.DistanceBetweenTwoPoints(startPos.transform, tile.transform);
                tile.H = _dungeonManager.DistanceBetweenTwoPoints(tile.transform, target.transform);

                // Add additional cost for diagonal movement
                int dx = (int)Mathf.Abs(tile.transform.position.x - startPos.gameObject.transform.position.x);
                int dy = (int)Mathf.Abs(tile.transform.position.y - startPos.gameObject.transform.position.y);
                if (dx == dy && dx > 0)
                {
                    tile.G += 0.5f; // Increase the cost for diagonal movement by 1
                }
            }
        }
        _currentNodes = tilesList;
    }

    // Creates a clone of the _currentTiles array
    // In order to prevent the actual tile data from changing
    private List<Tile> CloneCurrentTilesArray()
    {
        List<Tile> tileList = new List<Tile>();

        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            GameObject newObj = new GameObject();
            Tile newTile = newObj.AddComponent<Tile>();
            newObj.transform.position = tile.transform.position;
            newObj.name = tile.name;
            newTile.IsWalkable = tile.IsWalkable;
            newTile.G = tile.G;
            newTile.H = tile.H;
            tileList.Add(newTile);
            Destroy(newObj);
        }

        return tileList;
    }
}
