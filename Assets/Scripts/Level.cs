using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Level
{
    //name of the scene
    [SerializeField] private string levelScene;

    //unrelated to scene index in build
    [SerializeField] private int levelNumber;
}
