using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;

    private void Awake()
    {
        creditsPanel.SetActive(false);
    }

    public void ToLevelSelect()
    {
        MainManager.Instance.LoadLevel("level_select");
    }

    public void QuitApp()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
            Application.Quit();
    #endif
    }

    public void showCreditsPanel ()
    {
        creditsPanel.SetActive(true);
    }

    public void hideCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
