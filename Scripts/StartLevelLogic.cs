using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelLogic : MonoBehaviour
{
    public int LevelToStart;

    public void StartLevel()
    {
        MySceneManager.shared.GoToLevel(LevelToStart);
    }
}
