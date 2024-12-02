 using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private static int currentLevelNumber;
    [SerializeField] private static int nextLevelNumber;

    //Insert levels here
    [SerializeField] private static Level[] levels = {
        new Level("TestLevel", -2),
        new Level("MainTitleScene", -1),
        new Level("ShopLevel", 0),
        new Level("LevelOne", 1),
        new Level("LevelTwo", 2),
        new Level("LevelThree", 3),
        new Level("LevelFour", 4),
    };

    private static Level currentLevel;
    private static GameObject playerObj;
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
        LevelManager levelManagerScript = null;
        if (levelManagerObj != null)
        {
            levelManagerScript = levelManagerObj.GetComponent<LevelManager>();
        }
        
        if (PlayerScript.GetPlayerInstance() == null)
        {
            Instantiate(playerPrefab);
        }

        playerObj = GameObject.FindGameObjectWithTag("Player");

        currentLevel = FindInLevels(SceneManager.GetActiveScene().name);

        currentLevelNumber = currentLevel.GetLevelNumber();

        SwitchLevelTo(currentLevelNumber);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public int GetCurrentLevel()
    {
        return currentLevelNumber;
    }
    public static int GetNextLevelNumber()
    {
        foreach (Level level in levels)
        {
            if (level.GetCompletionState() != true && level.GetLevelNumber() > 0)
            {
                return level.GetLevelNumber();
            }
        }
        return 0;
    }

    public bool GetCurrentLevelState()
    {
        return currentLevel.GetCompletionState();
    }

    public static void SetCurrentLevelAsComplete()
    {
        currentLevel.SetComplete(true);
    }

    public static Level FindInLevels(int findLevelNumber)
    {
        return Level.FindLevel(levels, findLevelNumber);
    }
    public static Level FindInLevels(string findLevelName)
    {
        return Level.FindLevel(levels, findLevelName);
    }

    public static void SwitchLevelTo(int levelNum)
    {
        Level selectedLevel = Level.FindLevel(levels, levelNum);

        if (selectedLevel != null)
        {
            SwitchLevel(selectedLevel);
        }
    }

    public static void SwitchLevelTo(string levelName)
    {
        Level selectedLevel = Level.FindLevel(levels, levelName);

        if (selectedLevel != null)
        {
            SwitchLevel(selectedLevel);
        }
    }

    private static void SwitchLevel(Level switchToLevel)
    {
        currentLevelNumber = switchToLevel.GetLevelNumber();
        currentLevel = switchToLevel;
        string levelName = switchToLevel.GetLevelName();

        if (playerObj != null)
        {
            if (playerObj.GetComponent<PlayerScript>() != null)
            {
                PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();

                if (playerScript.GetCurrentHP() <= 0)
                {
                    Destroy(playerObj);

                    foreach (Level level in levels)
                    {
                        level.SetComplete(false);
                    }
                }
            }

            if (currentLevel.GetLevelName() == "MainTitleScene")
            {
                playerObj.SetActive(false);
            }
            else
            {
                playerObj.SetActive(true);
            }
        }
        SceneManager.LoadScene(levelName);
    }
}
