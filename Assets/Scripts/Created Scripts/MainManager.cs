using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Initialise MainManager as a singleton
    public static MainManager Instance { get; private set; }

    public bool isTimePaused { get; private set; } // Bool toggle time pausing

    private void Awake()
    {
        isTimePaused = false; // Make sure game is not paused on awake

        // Ensure only one instance of MainManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            Instance = this;
        }
        
        // Allow the MainManager to persist between scene loads
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isTimePaused)
        {
            Time.timeScale = 0;
        } 
        else
        {
            Time.timeScale = 1;
        }
    }

    // Function to load the specified scene
    public void LoadLevel(string levelname)
    {
        isTimePaused = false; // Automatically unpause whenever we load another level
        SceneManager.LoadScene(levelname);
    }

    // Function to toggle isPaused bool
    public void TogglePauseTime()
    {
        isTimePaused = !isTimePaused;
    }

    // Function to toggle isPaused bool
    public void PauseTime()
    {
        isTimePaused = true;
    }

    public void UnpauseTime()
    {
        isTimePaused = false;
    }
}
