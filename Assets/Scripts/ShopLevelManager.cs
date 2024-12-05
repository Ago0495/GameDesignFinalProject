using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelManager : LevelManager
{
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        
        if (gameManager != null )
        {
            int nextLevel = GameManager.GetNextLevelNumber();
            if ( nextLevel <= dialogueOptions.dialogueList.Length)
            {
                dialogueOptions.PickDialogue(nextLevel - 1);
            }

            FindAnyObjectByType<DialogueTrigger>().TriggerDialogue();

            if (GameManager.FindInLevels(nextLevel).GetLevelName() == "BossLevel")
            {
                GameObject dummy = GameObject.Find("Dummy");
                dummy.transform.position = GameObject.Find("ShopKeeperSpot").transform.position;
                dummy.GetComponent<EnemyScript>().SetTarget(GameObject.Find("ShopKeeperSpot"));

                GameObject.Find("ShopKeeper").SetActive(false);
            }

            if (nextLevel == dialogueOptions.dialogueList.Length)
            {
                GameObject barKeeper = GameObject.Find("BarKeeper");
                barKeeper.transform.position = GameObject.Find("ShopKeeperSpot").transform.position;
                barKeeper.GetComponent<EnemyScript>().SetTarget(GameObject.Find("ShopKeeperSpot"));

                GameObject.Find("ShopKeeper").SetActive(false);
                GameObject.Find("Dummy").SetActive(false);
            }
            else
            {
                GameObject barKeeper = GameObject.Find("BarKeeper");
                barKeeper.SetActive(false);
            }

            if (GameManager.GetPreviouisLevel().GetLevelName() == "MainTitleScene")
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    GameObject spawnpoint = GameObject.FindGameObjectWithTag("TitleSpawnPoint");

                    if (spawnpoint != null)
                    {
                        playerObj.transform.position = spawnpoint.transform.position;
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (FindAnyObjectByType<DialogueManager>() != null)
        {
            if (FindAnyObjectByType<DialogueManager>().finishedDialogue)
            {
                SetLevelComplete(true);
            }
        }
    }
}
