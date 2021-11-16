using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen shared;

    public GameObject Screen;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void EnableGameOver()
    {
        Time.timeScale = 0f;
        Screen.SetActive(true);
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
}
