using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    public void TogglePause()
    {
        MainManager.Instance.TogglePause();
    }

    public void ToMainMenu()
    {
        MainManager.Instance.LoadLevel("main_menu");
    }

    private void Update()
    {
        if (MainManager.Instance.isPaused == true)
        {
            pauseMenu.SetActive(true);
        } else
        {
            pauseMenu.SetActive(false);
        }
    }
}
