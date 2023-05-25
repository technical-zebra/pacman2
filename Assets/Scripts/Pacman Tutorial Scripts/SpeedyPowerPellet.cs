using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyPowerPellet : PowerPellet
{
    // Any additional properties or methods specific to SpeedyPowerPellet can be defined here

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

