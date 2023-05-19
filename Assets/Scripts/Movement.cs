using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // Movement speed
    public float speed = 8f;

    // Speed multiplier for power-ups or other effects
    public float speedMultiplier = 1f;

    // Initial movement direction
    public Vector2 initialDirection;

    // Layer mask for obstacle detection
    public LayerMask obstacleLayer;

    // Reference to the Rigidbody2D component
    public new Rigidbody2D rigidbody { get; private set; }

    // Current movement direction
    public Vector2 direction { get; private set; }

    // Next movement direction
    public Vector2 nextDirection { get; private set; }

    // Starting position of the object
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        // Get the reference to the Rigidbody2D component
        rigidbody = GetComponent<Rigidbody2D>();

        // Store the starting position of the object
        startingPosition = transform.position;
    }

    private void Start()
    {
        // Reset the movement state
        ResetState();
    }

    /// <summary>
    /// Resets the movement state to its initial values.
    /// </summary>
    public void ResetState()
    {
        // Reset the speed multiplier
        speedMultiplier = 1f;

        // Set the initial direction
        direction = initialDirection;

        // Clear the next direction
        nextDirection = Vector2.zero;

        // Reset the position to the starting position
        transform.position = startingPosition;

        // Enable the Rigidbody2D component
        rigidbody.isKinematic = false;

        // Enable the Movement script
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements more responsive
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        // Move the object based on the current direction and speed
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

    /// <summary>
    /// Sets the movement direction if the tile in that direction is available.
    /// </summary>
    /// <param name="direction">The desired movement direction.</param>
    /// <param name="forced">Flag indicating whether the direction change is forced.</param>
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // Otherwise, set it as the next direction so it'll be automatically set when it becomes available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

     /// <summary>
    /// Checks if the tile in the specified direction is occupied by an obstacle.
    /// </summary>
    /// <param name="direction">The direction to check for an obstacle.</param>
    /// <returns>True if the tile is occupied, false otherwise.</returns>
    public bool Occupied(Vector2 direction)
    {
        // Cast a box-shaped raycast in the specified direction to detect obstacles
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        
        // If a collider is hit, there is an obstacle in that direction
        return hit.collider != null;
    }

}
