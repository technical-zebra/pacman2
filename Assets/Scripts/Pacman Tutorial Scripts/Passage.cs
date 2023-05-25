using UnityEngine;

/// <summary>
/// Represents a passage in the game that teleports the player to a different location.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection; // The connection point to teleport the player to

    /// <summary>
    /// Called when an object enters the passage's collider.
    /// Teleports the object to the position of the connection point, maintaining its z-coordinate.
    /// </summary>
    /// <param name="other">The collider of the object that entered the passage.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the position of the connection point and set its z-coordinate to match the object's z-coordinate
        Vector3 position = connection.position;
        position.z = other.transform.position.z;
        other.transform.position = position; // Teleport the object to the new position
    }
}

