using UnityEngine;

/// <summary>
/// Represents a pellet in the game.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10; // Points gained when the pellet is eaten

    /// <summary>
    /// Called when the pellet is eaten.
    /// Triggers the PelletEaten method in the GameManager.
    /// </summary>
    protected virtual void Eat()
    {
        // Find the GameManager instance and invoke the PelletEaten method, passing itself as the parameter
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the Pacman
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            Eat(); // Call the Eat method to handle the pellet being eaten
        }
    }
}

