using UnityEngine;

/// <summary>
/// Represents the frightened behavior of a ghost.
/// </summary>
public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    /// <summary>
    /// Enables the frightened behavior with the specified duration.
    /// </summary>
    /// <param name="duration">The duration of the frightened behavior.</param>
    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / 2f);
    }

    /// <summary>
    /// Disables the frightened behavior.
    /// </summary>
    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    /// <summary>
    /// Handles the ghost being eaten.
    /// </summary>
    private void Eaten()
    {
        eaten = true;
        ghost.SetPosition(ghost.home.inside.position);
        ghost.home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    /// <summary>
    /// Flashes between blue and white sprites during the frightened behavior.
    /// </summary>
    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    /// <summary>
    /// Called when the script is enabled.
    /// Resets the animation and speed of the ghost.
    /// </summary>
    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    /// <summary>
    /// Called when the script is disabled.
    /// Resets the speed and eaten state of the ghost.
    /// </summary>
    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }

    /// <summary>
    /// Called when the ghost's collider enters another collider trigger.
    /// Sets the direction for the ghost based on available directions and the farthest distance from the target.
    /// </summary>
    /// <param name="other">The collider the ghost's collider entered.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is greater than the current max distance, then this direction becomes the new farthest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

        /// <summary>
    /// Called when the ghost's collider collides with another collider.
    /// Handles the collision with Pacman, triggering the "eaten" state if the ghost is enabled.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }
}

