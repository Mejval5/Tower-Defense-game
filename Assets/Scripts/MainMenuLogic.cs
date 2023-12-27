using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public float TimeFlow = 0.2f;

    void Awake()
    {
        Time.timeScale = TimeFlow;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
