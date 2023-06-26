using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a power pellet in the game.
/// </summary>
public class PowerPellet : Pellet
{
    public float duration = 8f; // Duration of the power pellet effect

    /// <summary>
    /// Overrides the Eat method from the base class.
    /// Triggers the PowerPelletEaten method in the GameManager and notifies the observers.
    /// </summary>
    protected override void Eat()
    {
        // Find the GameManager instance and invoke the PowerPelletEaten method, passing itself as the parameter
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
        NotifyObservers();
    }
}
