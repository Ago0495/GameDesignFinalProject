using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOptions : MonoBehaviour
{
    public Dialogue[] dialogueList;

    public void PickDialogue(int dialogueNumber)
    {
        if (dialogueList.Length > 0 && dialogueNumber < dialogueList.Length)
        {
            GetComponent<DialogueTrigger>().SetDialogue(dialogueList[dialogueNumber]);
        }
    }
}
