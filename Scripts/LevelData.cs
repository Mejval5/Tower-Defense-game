using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Datascripts/LevelData")]
public class LevelData : ScriptableObject
{
    public string[] Levels;
    public string MainMenuName;

    public string GetLevel(int levelID)
    {
        if (levelID < Levels.Length && levelID >= 0)
            return Levels[levelID];
        else
            return MainMenuName;
    }
}
