using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [Header("Starting Room")]
    private Room _startingRoom;
    private Transform[] _playerSpawnPoints;


    private void Start()
    {
        LoadNewArea();
    }

    private void LoadNewArea()
    {
        
    }
}
