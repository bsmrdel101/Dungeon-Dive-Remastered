using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float _cameraDefaultSpeed = 50f;
    [SerializeField] private float _cameraFastSpeed = 90f;
    private float _horizontalMove, _verticalMove;
    private float _mouseScroll = 0f;
    private float _cameraSpeed;

    [Header("Camera Zoom")]
    [SerializeField] private float _zoomAmount = 0.5f;
    [SerializeField] private float _cameraMin = 8;
    [SerializeField] private float _cameraMax = 20;
    [SerializeField] private float _cameraDefaultZoom = 6f;
    private CinemachineVirtualCamera _playerCamera;


    private void Start()
    {
        _cameraSpeed = _cameraDefaultSpeed;
        _playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        GetInputs();
        HandleCameraZoom();
        HandlePressShiftKey();

        // Move camera
        transform.position += new Vector3(_horizontalMove, _verticalMove, 0) * Time.deltaTime * _cameraSpeed;
    }

    private void GetInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
        _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
    }

    private void HandlePressShiftKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) _cameraSpeed = _cameraFastSpeed;
        if (Input.GetKeyUp(KeyCode.LeftShift)) _cameraSpeed = _cameraDefaultSpeed;
    }

    private void HandleCameraZoom()
    {
        if (_mouseScroll > 0 && _playerCamera.m_Lens.OrthographicSize > _cameraMin)
        {
            _playerCamera.m_Lens.OrthographicSize -= _zoomAmount;
        } else if (_mouseScroll < 0 && _playerCamera.m_Lens.OrthographicSize < _cameraMax)
        {
            _playerCamera.m_Lens.OrthographicSize += _zoomAmount;
        }
    }

    // Reset camera zoom
    public void ResetCameraZoom()
    {
        _playerCamera.m_Lens.OrthographicSize = _cameraDefaultZoom;
    }
}
