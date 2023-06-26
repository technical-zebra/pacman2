using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Define the event delegate
public delegate void PowerPelletEatenEventHandler(PowerPellet pellet);

public class GameManager : MonoBehaviour
{
    private Ghost[] ghosts;  // Array of ghost objects
    public Pacman pacman;  // Pacman object
    public Transform pellets;  // Transform containing pellet objects

    public TextMeshProUGUI livesText; // Text object to display the number of lives
    public TextMeshProUGUI scoreText; // Text object to display the score

    public int ghostMultiplier { get; private set; } = 1;  // Multiplier for ghost points
    public int score { get; private set; }  // Current score
    public int lives { get; private set; }  // Remaining lives

    public bool isGameOver { get; private set; } // Bool for if the game has ended
    public bool isGamePaused { get; private set; } // Bool for if the game has paused

    private bool pacmanInvincible = false; // Flag to track Pacman's invincibility

    public AudioSource eatpellet;
    public AudioSource eatpowerpellet;
    public AudioSource soundtrack;
    public AudioSource lostlife;
    public AudioSource ghostscream;
    public event PowerPelletEatenEventHandler PowerPelletEatenEvent;

    private void Start()
    {
        ghosts = FindObjectsOfType<Ghost>(self);
        NewGame();  // Start a new game
    }

    public void NewGame()
    {
        isGameOver = false;
        SetScore(0);  // Reset the score to 0
        SetLives(3);  // Set initial number of lives to 3
        NewRound();  // Start a new round

        MainManager.Instance.UnpauseTime(); // Call Main Manager to start time
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);  // Activate all the pellets
        }

        // Reset the state of ghosts and Pacman
        ResetState();
    }

    private void ResetState()
    {
        ResetPacmanState();
        ResetGhostState();
    }

    private void ResetPacmanState()
    {
        pacman.ResetState();  // Reset the state of Pacman
    }

    private void ResetGhostState()
    {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].ResetState();  // Reset the state of each ghost
            }
    }

    private void GameOver()
    {
        isGameOver = true;
        isGamePaused = false;

        for (int i = 0; i < ghosts.Length; i++) 
        {
            ghosts[i].gameObject.SetActive(false);  // Deactivate all the ghosts
        }  

        pacman.gameObject.SetActive(false);  // Deactivate Pacman

        MainManager.Instance.PauseTime(); // Call Main Manager to stop time
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();  // Update the lives text
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');  // Update the score text with zero-padding
    }

    public void PacmanEaten()
    {
        if (pacmanInvincible)
            return; // Return early if Pacman is invincible

        lostlife.Play();

        pacman.DeathSequence();  // Perform the death sequence for Pacman

        SetLives(lives - 1);  // Decrease the number of lives by 1

        if (lives > 0) {
            // Reset the state after a delay if there are remaining lives
            Invoke(nameof(ResetState), 3f);
        } else {
            Invoke(nameof(GameOver), 3f); // If no lives are remaining, trigger the game over sequence after a short delay
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        ghostscream.Play();

        int points = ghost.points * ghostMultiplier;  // Calculate the points earned by eating the ghost
        SetScore(score + points);  // Update the score
        ghostMultiplier++;  // Increase the ghost point multiplier
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);  // Deactivate the eaten pellet

        eatpellet.Play();
        SetScore(score + pellet.points);  // Update the score by adding the points of the eaten pellet

        if (!HasRemainingPellets())
        {
            GameOver();
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        // for (int i = 0; i < ghosts.Length; i++)
        // {
        //     ghosts[i].frightened.Enable(pellet.duration);  // Enable the frightened state of each ghost for the specified duration
        // }

        PowerPelletEatenEvent?.Invoke(this, new PowerPelletEatenEventArgs(pellet));

        eatpowerpellet.Play();
        soundtrack.Play();

        PelletEaten(pellet);  // Process the power pellet as a regular pellet
        CancelInvoke(nameof(ResetGhostMultiplier));  // Cancel any previous invocation of ResetGhostMultiplier
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);  // Reset the ghost point multiplier after the specified duration
    }

    public class PowerPelletEventArgs : EventArgs
    {
        public PowerPellet PowerPellet { get; }

        public PowerPelletEventArgs(PowerPellet powerPellet)
        {
            PowerPellet = powerPellet;
        }
    }

    public void SpeedyPowerPelletEaten(SpeedyPowerPellet pellet)
    {
        eatpowerpellet.Play();
        soundtrack.Play();
        PelletEaten(pellet);  // Process the speedy pellet as a regular pellet

        pacman.changeColor(new Color(1f, 0.7f, 0f));
        pacman.movement.SetSpeedMultiplier(1.5f, pellet.duration);  // Set the speed multiplier for Pacman
        
        CancelInvoke(nameof(DisablePacmanSpeedyVFX));
        Invoke(nameof(DisablePacmanSpeedyVFX), pellet.duration);
    }

    public void InvinciblePowerPelletEaten(InvinciblePowerPellet pellet)
    {
        eatpowerpellet.Play();
        soundtrack.Play();
        PelletEaten(pellet);  // Process the invincibile pellet as a regular pellet

        pacman.changeColor(new Color(0.15f, 0.4f, 1f));
        EnablePacmanInvincibility(pellet.duration); // Enable invincibility
        
        CancelInvoke(nameof(DisablePacmanInvincibility));
        Invoke(nameof(DisablePacmanInvincibility), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;  // Return true if there is at least one active pellet remaining
            }
        }

        return false;  // Return false if no active pellets are found
    }

    public void EnablePacmanInvincibility(float duration)
    {
        pacmanInvincible = true;
        Invoke(nameof(DisablePacmanInvincibility), duration);
    }

    private void DisablePacmanInvincibility()
    {
        pacman.resetColor();  // Reset Pacman color
        pacmanInvincible = false; // Reset Pacman's invincibility
    }

    private void DisablePacmanSpeedyVFX()
    {
        pacman.resetColor();  // Reset Pacman color
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;  // Reset the ghost point multiplier to 1
    }

    public void toggleGamePause()
    {
        if (!isGameOver)
        {
            MainManager.Instance.TogglePauseTime();
            isGamePaused = MainManager.Instance.isTimePaused;
        }
    }
}