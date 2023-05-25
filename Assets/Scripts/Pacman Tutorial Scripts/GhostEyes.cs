using UnityEngine;
/// <summary>
/// Controls the sprite and appearance of the ghost's eyes based on the movement direction.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    /// <summary>
    /// Reference to the SpriteRenderer component attached to the object.
    /// </summary>
    public SpriteRenderer spriteRenderer { get; private set; }

    /// <summary>
    /// Reference to the Movement component of the ghost.
    /// </summary>
    public Movement movement { get; private set; }

    private void Awake()
    {
        // Get the SpriteRenderer component attached to the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the Movement component attached to the parent object
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        // Update the sprite of the eyes based on the movement direction
        if (movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }
}
