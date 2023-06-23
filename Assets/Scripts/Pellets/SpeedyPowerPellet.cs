using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a speedy power pellet in the game.
/// </summary>
public class SpeedyPowerPellet : PowerPellet
{
    public SpeedyPowerPellet()
    {
        duration = 8f; // Duration of the power pellet effect
    }

    /// <summary>
    /// Overrides the Eat method from the base class.
    /// Triggers the SpeedyPowerPelletEaten method in the GameManager.
    /// </summary>
    protected override void Eat()
    {
        // Find the GameManager instance and invoke the SpeedyPowerPelletEaten method, passing itself as the parameter
        FindObjectOfType<GameManager>().SpeedyPowerPelletEaten(this);
    }
}
