using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public void ToMainMenu()
    {
        MainManager.Instance.LoadLevel("main_menu");
    }

    public void ToLevel01()
    {
        MainManager.Instance.LoadLevel("level01");
    }
}
