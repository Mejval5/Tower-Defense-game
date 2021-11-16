using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenLogic : MonoBehaviour
{
    public static WinScreenLogic shared;

    public GameObject Screen;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void EnableWinScreen()
    {
        Time.timeScale = 0f;
        Screen.SetActive(true);
    }

    public void DisableWinScreen()
    {
        Time.timeScale = 1f;
        Screen.SetActive(false);
    }

    public void RestartLevel()
    {
        MySceneManager.shared.RestartCurrentLevel();
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        MySceneManager.shared.GoToMenu();
    }

    public void GoToNextLevel()
    {
        MySceneManager.shared.GoToNextLevel();
    }
}
