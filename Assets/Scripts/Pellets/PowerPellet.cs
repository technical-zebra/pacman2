using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a power pellet in the game.
/// </summary>
public class PowerPellet : Pellet
{
    public float duration = 8f; // Duration of the power pellet effect

    private List<IPowerPelletObserver> observers = new List<IPowerPelletObserver>();

    /// <summary>
    /// Adds an observer to the list of observers.
    /// </summary>
    /// <param name="observer">The observer to add.</param>
    public void AddObserver(IPowerPelletObserver observer)
    {
        observers.Add(observer);
    }

    /// <summary>
    /// Removes an observer from the list of observers.
    /// </summary>
    /// <param name="observer">The observer to remove.</param>
    public void RemoveObserver(IPowerPelletObserver observer)
    {
        observers.Remove(observer);
    }

    /// <summary>
    /// Notifies all observers that the power pellet has been eaten.
    /// </summary>
    protected virtual void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnPowerPelletEaten(this);
        }
    }

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
