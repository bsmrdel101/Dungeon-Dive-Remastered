using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions_3()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile").transform;
        Transform tile2 = GameObject.Find("Floor_tile (16)").transform;
        yield return null;
        Assert.AreEqual(3, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions1_0()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile").transform;
        Transform tile2 = GameObject.Find("Floor_tile").transform;
        yield return null;
        Assert.AreEqual(0, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions2_5()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (31)").transform;
        Transform tile2 = GameObject.Find("Floor_tile (25)").transform;
        yield return null;
        Assert.AreEqual(5, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions3_2()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (8)").transform;
        Transform tile2 = GameObject.Find("Floor_tile (15)").transform;
        yield return null;
        Assert.AreEqual(2, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions4_4()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (30)").transform;
        Transform tile2 = GameObject.Find("Floor_tile (23)").transform;
        yield return null;
        Assert.AreEqual(4, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions3_3()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (19)").transform;
        Transform tile2 = GameObject.Find("Floor_tile").transform;
        yield return null;
        Assert.AreEqual(3, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions4_5()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (25)").transform;
        Transform tile2 = GameObject.Find("Floor_tile (31)").transform;
        yield return null;
        Assert.AreEqual(5, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }

    [UnityTest]
    public IEnumerator DistanceBetweenTwoPoints_TilePositions5_5()
    {
        DungeonManager dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        Transform tile1 = GameObject.Find("Floor_tile (25)").transform;
        Transform tile2 = GameObject.Find("Floor_tile (35)").transform;
        yield return null;
        Assert.AreEqual(5, dungeonManager.DistanceBetweenTwoPoints(tile1, tile2));
    }
}
