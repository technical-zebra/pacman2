using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an interface for power pellet observers.
/// </summary>
public interface IPowerPelletObserver
{
    void OnPowerPelletEaten(PowerPellet powerPellet);
}