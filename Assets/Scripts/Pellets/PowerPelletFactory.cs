using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a factory class for creating power pellets.
/// </summary>
public static class PowerPelletFactory
{
    public static PowerPellet CreatePowerPellet(PelletType pelletType)
    {
        PowerPellet powerPellet = null;

        switch (pelletType)
        {
            case PelletType.Normal:
                powerPellet = new PowerPellet();
                break;
            case PelletType.Speedy:
                powerPellet = new SpeedyPowerPellet();
                break;
            case PelletType.Invincible:
                powerPellet = new InvinciblePowerPellet();
                break;
            default:
                Debug.LogError("Invalid power pellet type: " + pelletType);
                break;
        }

        return powerPellet;
    }
}

/// <summary>
/// Represents the types of power pellets.
/// </summary>
public enum PelletType
{
    Normal,
    Speedy,
    Invincible
}