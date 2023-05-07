using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [HideInInspector] public static GameObject PlayerToken;

    [Header("References")]
    [SerializeField] private PhotonView _gameManagerView;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _mainCamera;

    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!_gameManagerView.IsMine)
        {
            // _playerCamera.SetActive(false);
            // Destroy(_mainCamera);
        }

        // Spawn player
        // PlayerToken = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    // Returns if mouse has changed position or not
    public bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
}
