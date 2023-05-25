using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePowerPellet : PowerPellet
{

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

