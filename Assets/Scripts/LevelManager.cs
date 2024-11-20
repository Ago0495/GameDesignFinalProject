using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //variables
    [SerializeField] private RoomScript[] rooms;
    [SerializeField] private LevelExitScript levelExitScript;
    [SerializeField] private bool levelComplete;
    [SerializeField] private int roomsCleared;
    private Scene currentScene;
    private int levelNumber;
    private PlayerScript playerScript;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (levelExitScript == null)
        {
            GameObject levelExitObj = GameObject.FindGameObjectWithTag("LevelExit");
            if (levelExitObj != null )
            {
                levelExitScript = levelExitObj.GetComponent<LevelExitScript>();
            }
        }

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObj != null)
        {
            gameManager = gameManagerObj.GetComponent<GameManager>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerScript = playerObj.GetComponent<PlayerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (levelComplete)
        {
            SetLevelComplete(true);
        }
    }

    public int GetLevelNum()
    {
        return levelNumber;
    }

    public void SetLevelComplete(bool levelComplete)
    {
        this.levelComplete = levelComplete;
        OpenLevelExit();
    }
    
    public void OpenLevelExit()
    {
        levelExitScript.OpenExit();
    }

    public void ExitLevel()
    {
        gameManager.SwitchLevelTo(0);
    }
}
