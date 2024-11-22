using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private string sentence = "";

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public Animator playerDialogueBoxAnimator;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.GetName();

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count >= 0 && dialogueText.text.Length < sentence.Length)
        {
            StopAllCoroutines();
            dialogueText.text = sentence;
        }
        else
        {
            if (sentences.Count == 0)
            {
                playerDialogueBoxAnimator.SetBool("IsOpen", true);
                //EndDialogue();
                return;
            }

            sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        playerDialogueBoxAnimator.GetComponent<Animator>().SetBool("IsOpen", false);
    }
}
