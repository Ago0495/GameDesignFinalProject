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
    private PlayerScript playerScript;
    private bool dialogueComplete;
    // Start is called before the first frame update

    void Awake()
    {
        transform.tag = "LevelManager";

        levelComplete = false;

        //temp
        dialogueComplete = true;
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

            if (spawnpoint.Length > 0 && !levelComplete)
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
        //default
        SetLevelComplete(true);
    }

    public int GetLevelNum()
    {
        return levelNumber;
    }

    public void SetLevelComplete(bool levelComplete)
    {

        this.levelComplete = levelComplete;
        GameManager.SetCurrentLevelAsComplete();
        OpenLevelExit();
    }
    
    public void OpenLevelExit()
    {
        if ( levelExitScript != null)
        {
            levelExitScript.OpenExit();
        }
    }

    public void ExitLevel()
    {
        GameManager.SwitchLevelTo(0);
    }
}
