using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject pauseTint; // dark tint displayed when the game is stopped

    public GameObject pauseMenu;

    public GameObject gameOverMenu;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        pauseMenu.SetActive(false); // Ensure pause menu isn't displayed at launch
        gameOverMenu.SetActive(false); // Ensure game over menu isn't displayed at launch
    }

    private void Update()
    {
        // Display dark tint if game time is stopped
        if (MainManager.Instance.isTimePaused)
        {
            pauseTint.SetActive(true);
        }
        else
        {
            pauseTint.SetActive(false);
        }

        if (gameManager.isGameOver == false)
        {
            // If game is not over hide game over menu
            gameOverMenu.SetActive(false);

            // If game is paused display pause menu
            if (gameManager.isGamePaused == true)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
        else
        {
            // If game is over display only game over menu
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(true);
        }
    }

    public void ToMainMenu()
    {
        MainManager.Instance.LoadLevel("main_menu");
    }
}
