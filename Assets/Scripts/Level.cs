using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Level
{
    //name of the scene
    [SerializeField] private string levelName;

    //unrelated to scene index in build
    [SerializeField] private int levelNumber;

    //level complete
    [SerializeField] private bool levelComplete;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public string GetLevelName()
    {
        return levelName;
    }
    public bool GetCompletionState()
    {
        return levelComplete;
    }
    public void SetComplete(bool complete)
    {
        levelComplete = complete;
    }

    public static Level FindLevel(Level[] levels, int number)
    {
        foreach (Level level in levels)
        {
            if (level.levelNumber == number)
            {
                return level;
            }
        }

        return null;
    }

    public static Level FindLevel(Level[] levels, string name)
    {
        foreach (Level level in levels)
        {
            if (level.levelName == name)
            {
                return level;
            }
        }

        return null;
    }
}
