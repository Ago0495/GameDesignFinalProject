using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOptions : MonoBehaviour
{
    public string entityName;
    public Dialogue[] dialogueList;

    void Start()
    {
        foreach (var dialogue in dialogueList)
        {
            dialogue.SetName(entityName);
        }
    }
    public void PickDialogue(int dialogueNumber)
    {
        if (dialogueList.Length > 0 && dialogueNumber < dialogueList.Length)
        {
            GetComponent<DialogueTrigger>().SetDialogue(dialogueList[dialogueNumber]);
        }
    }
}
