using UnityEngine;
/// <summary>
/// Base class for defining different behaviors of a ghost.
/// </summary>
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    /// <summary>
    /// Reference to the Ghost component attached to the object.
    /// </summary>
    public Ghost ghost { get; private set; }

    /// <summary>
    /// Duration of the behavior.
    /// </summary>
    public float duration;

    private void Awake()
    {
        // Get the Ghost component attached to the object
        ghost = GetComponent<Ghost>();
    }

    /// <summary>
    /// Enable the ghost behavior with the specified duration.
    /// </summary>
    public void Enable()
    {
        Enable(duration);
    }

    /// <summary>
    /// Enable the ghost behavior with a custom duration.
    /// </summary>
    /// <param name="duration">Duration of the behavior.</param>
    public virtual void Enable(float duration)
    {
       this.enabled = true; //NB

        // Cancel any previous invoke calls and schedule the behavior to disable after the specified duration
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    /// <summary>
    /// Disable the ghost behavior.
    /// </summary>
    public virtual void Disable()
    {
        // Disable the behavior by disabling the script
        this.enabled = false; //NB
        // Commented out since it's not needed as the script can be disabled by setting the 'enabled' property to false

        // Cancel any pending invoke calls
        CancelInvoke();
    }
}
