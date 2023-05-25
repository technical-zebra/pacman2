using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;  // Array of ghost objects
    public Pacman pacman;  // Pacman object
    public Transform pellets;  // Transform containing pellet objects

    public TextMeshProUGUI gameOverText; // Text object to display game over message
    public TextMeshProUGUI livesText; // Text object to display the number of lives
    public TextMeshProUGUI scoreText; // Text object to display the score

    public int ghostMultiplier { get; private set; } = 1;  // Multiplier for ghost points
    public int score { get; private set; }  // Current score
    public int lives { get; private set; }  // Remaining lives

    private void Start()
    {
        NewGame();  // Start a new game
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();  // Start a new game if all lives are lost and any key is pressed
        }
    }

    private void NewGame()
    {
        SetScore(0);  // Reset the score to 0
        SetLives(3);  // Set initial number of lives to 3
        NewRound();  // Start a new round
    }

    private void NewRound()
    {
        gameOverText.enabled = false;  // Hide the game over text

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);  // Activate all the pellets
        }

        ResetState();  // Reset the state of ghosts and Pacman
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();  // Reset the state of each ghost
        }

        pacman.ResetState();  // Reset the state of Pacman
    }

    private void GameOver()
    {
        gameOverText.enabled = true;  // Show the game over text

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);  // Deactivate all the ghosts
        }

        pacman.gameObject.SetActive(false);  // Deactivate Pacman
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
        pacman.DeathSequence();  // Perform the death sequence for Pacman

        SetLives(lives - 1);  // Decrease the number of lives by 1

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);  // Reset the state after a delay if there are remaining lives
        } else {
            GameOver();  // If no lives are remaining, trigger the game over sequence
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;  // Calculate the points earned by eating the ghost
        SetScore(score + points);  // Update the score

        ghostMultiplier++;  // Increase the ghost point multiplier
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);  // Deactivate the eaten pellet

        SetScore(score + pellet.points);  // Update the score by adding the points of the eaten pellet

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);  // Deactivate Pacman
            Invoke(nameof(NewRound), 3f);  // Start a new round after a delay if there are no remaining pellets
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);  // Enable the frightened state of each ghost for the specified duration
        }

        PelletEaten(pellet);  // Process the power pellet as a regular pellet
        CancelInvoke(nameof(ResetGhostMultiplier));  // Cancel any previous invocation of ResetGhostMultiplier
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);  // Reset the ghost point multiplier after the specified duration
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

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;  // Reset the ghost point multiplier to 1
    }
}