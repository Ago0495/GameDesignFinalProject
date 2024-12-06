using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private DialogueManager dialogueManager;
    private int roomsCleared = 0;
    private PlayerScript playerScript;
    private bool dialogueComplete;
    private int dialogueIndex;
    // Start is called before the first frame update

    void Awake()
    {
        transform.tag = "LevelManager";

        levelComplete = false;
        dialogueIndex = 0;
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

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        GameManager.UpdateGlobalVolume();
    }

    void Update()
    {
        if (dialogueManager != null)
        {
            PlayDialogue();
        }
    }

    public int GetLevelNum()
    {
        return levelNumber;
    }

    public void SetLevelComplete(bool levelComplete)
    {

        this.levelComplete = levelComplete;
        //Debug.Log(levelNumber);
        if (levelNumber == 5)
        {
            GameManager.SetGameCompleted(true);
        }
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

    private void PlayDialogue()
    {
        if (dialogueManager.finishedDialogue && dialogueIndex < dialogueOptions.dialogueList.Length)
        {
            dialogueOptions.PickDialogue(dialogueIndex);
            FindAnyObjectByType<DialogueTrigger>().TriggerDialogue();
            dialogueIndex++;
        }
    }
}
