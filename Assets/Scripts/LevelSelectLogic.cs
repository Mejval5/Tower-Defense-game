using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectLogic : MonoBehaviour
{
    public bool Reset = false;
    public int DebugLevelFinished = -1;

    private void Update()
    {
        if (Reset)
        {
            PlayerPrefs.SetInt("LevelFinished", DebugLevelFinished);
            UpdateButtons();
            Reset = false;
        }
    }


    void Awake()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        int LevelFinished = PlayerPrefs.GetInt("LevelFinished", -1);

        for (int i = 0; i < transform.childCount; i++)
        {
            bool isEnabled = LevelFinished + 1 >= i;
            transform.GetChild(i).GetComponent<Button>().interactable = isEnabled;
        }
    }

}
