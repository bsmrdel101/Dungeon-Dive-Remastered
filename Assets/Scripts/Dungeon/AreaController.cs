using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [Header("Starting Room")]
    private Room _startingRoom;
    private Transform[] _playerSpawnPoints;

    [Header("References")]
    private DungeonManager _dungeonManager;


    private void Start()
    {
        _dungeonManager = GameObject.FindObjectOfType<DungeonManager>();
        LoadNewArea();
    }

    private void LoadNewArea()
    {
        GetStartingRoomValues();
        SpawnRooms();
        SpawnPlayers();
    }

    // Get random room and set player spawn points
    private void GetStartingRoomValues()
    {
        _startingRoom = GetRandomStartingRoomFromFloor(_dungeonManager.Floor);
        _playerSpawnPoints = _startingRoom.PlayerSpawnPoints;
    }

    // Spawn rooms
    private void SpawnRooms()
    {
        // Initialize starting rooms
        Instantiate(_startingRoom.gameObject, Vector3.zero, Quaternion.identity);

        // Spawn the rest of the rooms
        int amountOfRooms = Random.Range(3, 6);
        Room prevRoom = _startingRoom;
        for (int i = 0; i < amountOfRooms; i++)
        {
            // Get a random room from this floor
            Room randomRoom = GetRandomRoomFromFloor(_dungeonManager.Floor);
            // Spawn the room and connect it to the previous room
            GameObject roomObj = Instantiate(randomRoom.gameObject, prevRoom.TopRoomConnection.position, Quaternion.identity);
            Room newRoom = roomObj.GetComponent<Room>();
            newRoom.transform.position = new Vector2(newRoom.transform.position.x - newRoom.BottomRoomConnection.localPosition.x, newRoom.transform.position.y - newRoom.BottomRoomConnection.localPosition.y);
            prevRoom = newRoom;
        }
    }

    // Spawn players
    private void SpawnPlayers()
    {
        GameManager.PlayerToken = PhotonNetwork.Instantiate("Player", _playerSpawnPoints[0].position, Quaternion.identity);
    }

    // Takes in a dungeon floor number
    // Returns a random starting room from that floor
    private Room GetRandomStartingRoomFromFloor(int floor)
    {
        if (floor == 1)
            return _dungeonManager.FloorOneStartingRooms[Random.Range(0, _dungeonManager.FloorOneStartingRooms.Length)].GetComponent<Room>();
        else if (floor == 2)
            return _dungeonManager.FloorTwoStartingRooms[Random.Range(0, _dungeonManager.FloorTwoStartingRooms.Length)].GetComponent<Room>();
        else
            return _dungeonManager.FloorThreeStartingRooms[Random.Range(0, _dungeonManager.FloorThreeStartingRooms.Length)].GetComponent<Room>();
    }

    // Takes in a dungeon floor number
    // Returns a random room from that floor
    private Room GetRandomRoomFromFloor(int floor)
    {
        if (floor == 1)
            return _dungeonManager.FloorOneRooms[Random.Range(0, _dungeonManager.FloorOneRooms.Length)].GetComponent<Room>();
        else if (floor == 2)
            return _dungeonManager.FloorTwoRooms[Random.Range(0, _dungeonManager.FloorTwoRooms.Length)].GetComponent<Room>();
        else
            return _dungeonManager.FloorThreeRooms[Random.Range(0, _dungeonManager.FloorThreeRooms.Length)].GetComponent<Room>();
    }
}
