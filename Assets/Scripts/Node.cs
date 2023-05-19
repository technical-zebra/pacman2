using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a node in the game grid for pathfinding purposes.
/// </summary>
public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer; // The layer mask for obstacle detection
    public List<Vector2> availableDirections { get; private set; } // List of available directions from the node

    private void Start()
    {
        availableDirections = new List<Vector2>();

        // We determine if the direction is available by box casting to see if we hit a wall.
        // The direction is added to the list if it's available.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    /// <summary>
    /// Checks if the given direction is available from the node and adds it to the available directions list if so.
    /// </summary>
    /// <param name="direction">The direction to check.</param>
    private void CheckAvailableDirection(Vector2 direction)
    {
        // Perform a box cast to check if there's an obstacle in the given direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, obstacleLayer);

        // If no collider is hit, then there is no obstacle in that direction
        if (hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }
}
