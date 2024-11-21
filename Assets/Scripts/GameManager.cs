 using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variables
    [SerializeField] private int currentLevelNumber;
    [SerializeField] private int nextLevelNumber;
    [SerializeField] private Level[] levels;
    private Level currentLevel;
    private Scene currentLevelScene;
    private PlayerScript playerScript;
    private static GameManager gameInstance;

    private void Awake()
    {
        transform.tag = "GameManager";

        DontDestroyOnLoad(this.gameObject);

        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        LevelManager levelManagerScript = levelManagerObj.GetComponent<LevelManager>();

        currentLevelNumber = levelManagerScript.GetLevelNum();
        nextLevelNumber = currentLevelNumber + 1;
        currentLevel = Level.FindLevel(levels, currentLevelNumber);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public int GetCurrentLevel()
    {
        return currentLevelNumber;
    }
    public void SetCurrentLevel(int lvl)
    {
        currentLevelNumber = lvl;
    }
    public int GetNextLevel()
    {
        return nextLevelNumber;
    }

    public bool GetCurrentLevelState()
    {
        return currentLevel.GetCompletionState();
    }

    public void SetCurrentLevelComplete()
    {
        currentLevel.SetComplete(true);
    }

    public void SwitchLevelTo(int levelNum)
    {
        currentLevelNumber = levelNum;

        if (currentLevelNumber == 1)
        {
            nextLevelNumber++;
        }

        Level selectedLevel = Level.FindLevel(levels, levelNum);


        if (selectedLevel != null)
        {
            currentLevel = selectedLevel;
            string levelName = selectedLevel.GetLevelName();
            SceneManager.LoadScene(levelName);
        }
    }
}
