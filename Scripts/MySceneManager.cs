using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager shared;

    public LevelData LevelNames;

    public string CurrentLevel;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }

    void Start()
    {
        CurrentLevel = SceneManager.GetActiveScene().name;
        Application.targetFrameRate = 300;
    }

    public void RestartCurrentLevel()
    {
        ScreenFaderLogic.shared.FadeIntoScene(CurrentLevel);
    }

    public void GoToMenu()
    {
        ScreenFaderLogic.shared.FadeIntoScene(LevelNames.MainMenuName);
    }
    public void GoToLevel(string levelName)
    {
        ScreenFaderLogic.shared.FadeIntoScene(levelName);
    }
    public void GoToLevel(int levelNumber)
    {
        GoToLevel(LevelNames.GetLevel(levelNumber));
    }

    public void StartFirstLevel()
    {
        GoToLevel(0);
    }

    public void GoToNextLevel()
    {
        int index = GetCurrentLevelIndex();

        GoToLevel(LevelNames.GetLevel(index + 1));
    }

    int GetCurrentLevelIndex()
    {
        for (int i = 0; i < LevelNames.Levels.Length; i++)
        {
            if (LevelNames.Levels[i] == CurrentLevel)
            {
                return i;
            }
        }
        return -1;
    }

    public void SaveProgress()
    {
        int index = GetCurrentLevelIndex();
        int highestLevelSoFar = PlayerPrefs.GetInt("LevelFinished", -1);

        if (highestLevelSoFar < index)
            PlayerPrefs.SetInt("LevelFinished", index);
    }
}
