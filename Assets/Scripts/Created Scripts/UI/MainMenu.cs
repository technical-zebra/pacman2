using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
}
