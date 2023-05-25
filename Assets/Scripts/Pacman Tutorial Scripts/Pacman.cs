using UnityEngine;

/// <summary>
/// Represents the player character, Pacman, in the game.
/// </summary>
[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public AnimatedSprite deathSequence; // The animated sprite representing the death sequence of Pacman
    public SpriteRenderer spriteRenderer { get; private set; } // The sprite renderer component of Pacman
    public new Collider2D collider { get; private set; } // The collider component of Pacman
    public Movement movement { get; private set; } // The movement component of Pacman
    public PlayerInput playerInput { get; private set; } // The player input component of Pacman

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // Get player directional input from input component
        Vector2 inputDirection = playerInput.GetDirectionalInput();
        // Pass player input from input component to movement component if the player presses a directional key
        if (inputDirection.magnitude > 0)
        {
            movement.SetDirection(playerInput.GetDirectionalInput());
        }

        // Rotate Pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    /// <summary>
    /// Resets the state of Pacman to its initial state.
    /// </summary>
    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.spriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Initiates the death sequence of Pacman.
    /// </summary>
    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.spriteRenderer.enabled = true;
        deathSequence.Restart();
    }
}
