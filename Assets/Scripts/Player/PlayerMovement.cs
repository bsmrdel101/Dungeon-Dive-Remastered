using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement")]
    [SerializeField] private int moveDistance = 5;
    private bool _dragging = false;
    private Vector3 _mousePos;
    private Tile _tileBeforeMove;
    private GameObject _ghostToken;
    private bool _validToken = false;

    [Header("References")]
    private GameManager _gameManager;
    private DungeonManager _dungeonManager;
    private Pathfinding _pathfinding;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private PhotonView _playerView;


    private void Start()
    {
        if (!_playerView.IsMine) return;
        // If there is some weird problem with colliders it's probably here
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
        _pathfinding = _dungeonManager.GetComponent<Pathfinding>();

        // < DELETE THIS >
        // Temporary sets the PlayerToken varriable to a hard coded gameobject
        // GameManager.PlayerToken = GameObject.FindGameObjectWithTag("Ally");
    }

    private void Update()
    {
        if (!_playerView.IsMine) return;
        HandlePlayerMovement();
    }

    // Detects player movement inputs
    private void HandlePlayerMovement()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, -Vector2.up);

        // Detect player start dragging
        if (Input.GetMouseButtonDown(0))
        {
            _validToken = IsValidToken(hit);
            if (!_validToken) return;
            // Set this tile as the start position for dragging
            _tileBeforeMove = _dungeonManager.GetTileFromCoords(hit.collider.transform.position.x, hit.collider.transform.position.y);
            TokenDragStart(hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite, hit.collider.gameObject.transform);
        }

        // Detect while player is dragging
        if (_dragging && _gameManager.HasMouseMoved()) TokenDrag();

        // Detect player stop dragging
        if (Input.GetMouseButtonUp(0) && _validToken) TokenDragEnd(hit);

        // Reset camera position when space is pressed
        if (Input.GetKeyDown(KeyCode.Space)) ResetPlayerCameraPos();
    }

    private void TokenDragStart(Sprite sprite, Transform transform)
    {
        _dragging = true;
        // Spawn ghost token
        GameObject obj = new GameObject("GhostToken");
        _ghostToken = Instantiate(obj, transform.position, Quaternion.identity);
        // Add sprite renderer to ghost token
        SpriteRenderer spriteRenderer = _ghostToken.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 3;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Delete duplicate object
        Destroy(obj);
    }

    private void TokenDrag()
    {
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, -Vector2.up);
        if (!hit || !IsValidMovement(hit.collider.GetComponent<Surface>(), hit)) return;

        Vector3 hitPosition = hit.collider.transform.position;
        float x = hitPosition.x;
        float y = hitPosition.y;
        // Clamp move distance
        if (_dungeonManager.DistanceBetweenTwoPoints(transform, hit.collider.gameObject.transform) > moveDistance)
        {
            float minX = transform.position.x - moveDistance;
            float minY = transform.position.y - moveDistance;
            x = Mathf.Clamp(x, minX, transform.position.x + moveDistance);
            y = Mathf.Clamp(y, minY, transform.position.y + moveDistance);
        }

        if (_dungeonManager.DistanceBetweenTwoPoints(transform, hit.collider.transform) > moveDistance)
        {
            // Move it to clamped distance if target is not a valid place to move
            _ghostToken.transform.SetParent(_dungeonManager.GetTileFromCoords(x, y).transform);
            _ghostToken.transform.localPosition = Vector3.zero;
        } else
        {
            // Move the target normally
            _ghostToken.transform.SetParent(hit.collider.gameObject.transform);
            _ghostToken.transform.localPosition = Vector3.zero;
        }
    }

    private void TokenDragEnd(RaycastHit2D hit)
    {
        _dragging = false;
        _validToken = false;
        _pathfinding.GetTargetPath(_tileBeforeMove, _ghostToken.gameObject.GetComponentInParent<Tile>());
        MoveToTile();
        Destroy(_ghostToken);
    }

    // Moves token to selected position
    private void MoveToTile()
    {
        Vector3 prevCameraPos = _cameraController.gameObject.transform.position;
        transform.position = _ghostToken.transform.position;
        // Only move camera with token if it's allowed to
        if (!CameraController.CameraFollowToken) _cameraController.gameObject.transform.position = prevCameraPos;
    }

    // Checks if the token being selected can be moved by this player
    public bool IsValidToken(RaycastHit2D hit)
    {
        if (hit && hit.collider.gameObject == GameManager.PlayerToken)
            return true;
        else
            return false;
    }

    // Checks if a token movement is going to end up in a valid position
    private bool IsValidMovement(Surface surface, RaycastHit2D hit)
    {
        if (!surface || !surface.IsWalkable)
            return false;
        else
            return true;
    }

    // Reset camera position
    private void ResetPlayerCameraPos()
    {
        CameraController.CameraFollowToken = true;
        _cameraController.ResetCameraPos();
    }
}
