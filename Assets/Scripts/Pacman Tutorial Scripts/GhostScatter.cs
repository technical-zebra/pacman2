using UnityEngine;

/// <summary>
/// Represents the scatter behavior of a ghost.
/// </summary>
public class GhostScatter : GhostBehavior
{
    /// <summary>
    /// Called when the script is disabled.
    /// Enables the chase behavior of the ghost.
    /// </summary>
    private void OnDisable()
    {
        ghost.chase.Enable();
    }

    /// <summary>
    /// Called when the ghost's collider enters another collider.
    /// </summary>
    /// <param name="other">The collider that the ghost's collider entered.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the Node component from the collided object
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            // Pick a random available direction from the node's available directions
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back in the same direction, so if the randomly chosen direction is the opposite of the current direction,
            // increment the index to the next available direction
            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -ghost.movement.direction)
            {
                index++;

                // Wrap the index back around if it exceeds the available directions count
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            // Set the ghost's movement direction to the chosen direction
            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
