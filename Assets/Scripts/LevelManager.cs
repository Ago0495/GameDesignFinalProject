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
    [SerializeField] private protected DialogueOptions dialogueOptions;
    private int roomsCleared = 0;
    private PlayerScript playerScript;
    private bool dialogueComplete;
    // Start is called before the first frame update

    void Awake()
    {
        transform.tag = "LevelManager";

        levelComplete = false;
    }
    protected virtual void Start()
    {
        if (levelExitScript == null)
        {
            GameObject[] levelExitObj = GameObject.FindGameObjectsWithTag("LevelExit");
            if (levelExitObj != null)
            {
                foreach (var exit in levelExitObj)
                {
                    if (exit.GetComponent<LevelExitScript>().goToLevel > 0)
                    {
                        levelExitScript = exit.GetComponent<LevelExitScript>();
                        break;
                    }
                }
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
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");

        if (dialogueOptions != null)
        {
            dialogueOptions.PickDialogue(0);
            FindAnyObjectByType<DialogueTrigger>().TriggerDialogue();
        }
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
    public void clearedRoom()
    {
        roomsCleared++;
        if (roomsCleared == rooms.Length)
        {
            SetLevelComplete(true);
        }
    }
}
