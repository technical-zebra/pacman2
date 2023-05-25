using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Initialise MainManager as a singleton
    public static MainManager Instance { get; private set; }

    public bool isPaused = false; // Bool toggle game pausing

    private void Awake()
    {
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
        // If game is paused set timescale to 0 to pause game
        if (isPaused)
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
        SceneManager.LoadScene(levelname);
    }

    // Function to toggle isPaused bool
    public void TogglePause()
    {
        isPaused = !isPaused;
    }
}
