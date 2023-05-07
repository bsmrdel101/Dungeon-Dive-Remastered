using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Developer Tools")]
    [SerializeField] private bool _debugMode = false;

    [Header("References")]
    [SerializeField] private GameObject _lobbyCanvas;
    [SerializeField] private GameObject _roomCanvas;
    [SerializeField] private PhotonView _view;


    private void Start()
    {
        if (_debugMode)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        } else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {   
        PhotonNetwork.JoinOrCreateRoom("dev", new RoomOptions{ MaxPlayers = 4, BroadcastPropsChangeToAll = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        _lobbyCanvas.SetActive(false);
        _roomCanvas.SetActive(true);
    }

    public void OnClickStartGame()
    {
        _view.RPC("HandleStartGame_RPC", RpcTarget.All);
    }

    [PunRPC]
    private void HandleStartGame_RPC()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
