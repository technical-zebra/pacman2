using System.Collections;
using UnityEngine;

/// <summary>
/// Represents the home behavior of a ghost.
/// </summary>
public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    /// <summary>
    /// Called when the script is enabled.
    /// Stops all coroutines.
    /// </summary>
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Called when the script is disabled.
    /// Initiates the exit transition coroutine if the object is still active.
    /// </summary>
    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    /// <summary>
    /// Called when the ghost's collider collides with another collider.
    /// Reverses the ghost's direction when hitting a wall to create a bouncing effect.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction every time the ghost hits a wall to create the effect of the ghost bouncing around the home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }

    /// <summary>
    /// Coroutine for the exit transition of the ghost from the home.
    /// </summary>
    /// <returns>An IEnumerator used for coroutine execution.</returns>
    private IEnumerator ExitTransition()
    {
        // Turn off movement while we manually animate the position
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.rigidbody.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Pick a random direction left or right and re-enable movement
        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.movement.rigidbody.isKinematic = false;
        ghost.movement.enabled = true;
    }
}
