using UnityEngine;

/// <summary>
/// Represents a ghost in the game.
/// </summary>
[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour, IPowerPelletObserver
{
    /// <summary>
    /// Reference to the Movement component attached to the ghost.
    /// </summary>
    public Movement movement { get; private set; }

    /// <summary>
    /// Reference to the GhostHome component attached to the ghost.
    /// </summary>
    public GhostHome home { get; private set; }

    /// <summary>
    /// Reference to the GhostScatter component attached to the ghost.
    /// </summary>
    public GhostScatter scatter { get; private set; }

    /// <summary>
    /// Reference to the GhostChase component attached to the ghost.
    /// </summary>
    public GhostChase chase { get; private set; }

    /// <summary>
    /// Reference to the GhostFrightened component attached to the ghost.
    /// </summary>
    public GhostFrightened frightened { get; private set; }

    /// <summary>
    /// Initial behavior of the ghost.
    /// </summary>
    public GhostBehavior initialBehavior;

    /// <summary>
    /// Target transform for the ghost's behavior.
    /// </summary>
    public Transform target;

    /// <summary>
    /// Points awarded for eating this ghost.
    /// </summary>
    public int points = 200;

    public Ghost(Game game)
    {
        game.PowerPelletEatenEvent += OnPowerPelletEaten;
    }

    private void PowerPelletEatenEventHandler(object sender, PowerPelletEatenEventArgs e)
    {
        self.frightened.Enable(e.pellet.duration);
    }

    private void Awake()
    {
        // Get the required components attached to the object
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        // Reset the state of the ghost
        ResetState();

        // Subscribe to the PowerPelletEatenEvent
        game.PowerPelletEatenEvent += OnPowerPelletEaten;
    }

    /// <summary>
    /// Reset the state of the ghost.
    /// </summary>
    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        // Disable the home behavior if it's not the initial behavior
        if (home != initialBehavior)
        {
            home.Disable();
        }

        // Enable the initial behavior if it's specified
        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    /// <summary>
    /// Set the position of the ghost.
    /// </summary>
    /// <param name="position">The new position of the ghost.</param>
    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            // Check if the ghost is currently in frightened mode
            if (frightened.enabled)
            {
                // Notify the GameManager that the ghost has been eaten
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                // Notify the GameManager that Pacman has been eaten
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
