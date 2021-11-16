using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour
{
    public static PauseMenuLogic shared;

    public GameObject Screen;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void EnablePauseMenu()
    {
        Time.timeScale = 0f;
        Screen.SetActive(true);
    }

    public void DisablePauseMenu()
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
}
