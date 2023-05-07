using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Properties")]
    public Transform[] PlayerSpawnPoints;

    [Header("Room Connections")]
    public Transform TopRoomConnection;
    public Transform BottomRoomConnection;
}
