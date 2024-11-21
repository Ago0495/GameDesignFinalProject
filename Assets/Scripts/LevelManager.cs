using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //variables
    [SerializeField] private int levelNumber;
    [SerializeField] private RoomScript[] rooms;
    [SerializeField] private LevelExitScript levelExitScript;
    [SerializeField] private bool levelComplete;
    [SerializeField] private int roomsCleared;
    private Scene currentScene;
    private PlayerScript playerScript;
    [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update

    void Awake()
    {
        levelComplete = false;
    }
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

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            GameObject[] spawnpoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
            playerScript = playerObj.GetComponent<PlayerScript>();

            if (spawnpoint != null && !levelComplete)
            {
                playerObj.transform.position = spawnpoint[0].transform.position;
            }
            else
            {
                playerObj.transform.position = Vector3.zero;
            }
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
