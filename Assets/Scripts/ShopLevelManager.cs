using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelManager : LevelManager
{
    [SerializeField] private DialogueOptions dialogueOptions;
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        
        if (gameManager != null )
        {
            int nextLevel = GameManager.GetNextLevelNumber();
            dialogueOptions.PickDialogue(nextLevel -1);

            FindAnyObjectByType<DialogueTrigger>().TriggerDialogue();
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
