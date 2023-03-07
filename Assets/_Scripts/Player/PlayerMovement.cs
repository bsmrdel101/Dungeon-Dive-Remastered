using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int moveDistance = 5;
    private bool _dragging = false;


    private void Update()
    {
        HandlePlayerMovement();
    }

    // Detects player movement inputs
    private void HandlePlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && IsValidMovement())
        {
            _dragging = true;
        }
    }

    // Checks if a token movement is going to end up in a valid position
    private bool IsValidMovement()
    {
        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        // Debug.Log(hit.collider.gameObject);
        // if (hit.collider.gameObject.tag != "Player") return false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, moveDistance, 1<<8);
        Debug.Log(hit.collider.gameObject);
        return true;
    }

    // Takes in a coordinate
    // Moves token to selected position
    private void MoveToTile()
    {

    }
}
