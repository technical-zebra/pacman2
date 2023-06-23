using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an invincible power pellet in the game.
/// </summary>
public class InvinciblePowerPellet : PowerPellet
{
    public InvinciblePowerPellet()
    {
        duration = 15f; // Duration of the power pellet effect
    }

    /// <summary>
    /// Overrides the Eat method from the base class.
    /// Triggers the InvinciblePowerPelletEaten method in the GameManager.
    /// </summary>
    protected override void Eat()
    {
        // Find the GameManager instance and invoke the InvinciblePowerPelletEaten method, passing itself as the parameter
        FindObjectOfType<GameManager>().InvinciblePowerPelletEaten(this);
    }
}

