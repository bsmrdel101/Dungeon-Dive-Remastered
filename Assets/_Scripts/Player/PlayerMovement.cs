using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int moveDistance = 5;
    private bool _dragging = false;
    private Vector3 _mousePos;
    private GameObject _ghostToken;

    [Header("References")]
    private GameManager _gameManager;
    private DungeonManager _dungeonManager;


    private void Start()
    {
        // If there is some weird problem with colliders it's probably here
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _dungeonManager = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
    }

    private void Update()
    {
        HandlePlayerMovement();
    }

    // Detects player movement inputs
    private void HandlePlayerMovement()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, -Vector2.up);

        // Detect player start dragging
        if (Input.GetMouseButtonDown(0) && IsValidToken()) TokenDragStart(hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite, hit.collider.gameObject.transform);

        // Detect while player is dragging
        if (_dragging && _gameManager.HasMouseMoved()) TokenDrag();

        // Detect player stop dragging
        if (Input.GetMouseButtonUp(0)) TokenDragEnd();
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
        // Only continue if the tile is a valid place to move
        if (!hit || hit.collider.tag == "Ally" || hit.collider.tag == "Enemy" || !IsValidMovement(hit.collider.gameObject.GetComponent<Surface>(), hit.collider.gameObject.transform)) return;
        _ghostToken.transform.SetParent(hit.collider.gameObject.transform);
        _ghostToken.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void TokenDragEnd()
    {
        _dragging = false;
        MoveToTile();
        Destroy(_ghostToken);
    }

    // Moves token to selected position
    private void MoveToTile()
    {
        transform.position = _ghostToken.transform.position;
    }

    // Checks if the token being selected can be moved by this player
    private bool IsValidToken()
    {
        return true;
    }

    // Checks if a token movement is going to end up in a valid position
    private bool IsValidMovement(Surface surface, Transform tilePos)
    {
        if (!surface.IsWalkable || _dungeonManager.DistanceBetweenTwoPoints(transform, tilePos) > moveDistance)
            return false;
        else
            return true;
    }
}
