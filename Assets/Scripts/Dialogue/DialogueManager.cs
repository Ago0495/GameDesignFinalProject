using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private string sentence = "";
    public bool finishedDialogue;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    private CameraScript cameraScript;

    private protected AudioSource dialogueSound;

    private void Awake()
    {
        finishedDialogue = true;
    }

    void Start()
    {
        sentences = new Queue<string>();
        cameraScript = FindAnyObjectByType<CameraScript>();
        dialogueSound = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        finishedDialogue = false;

        cameraScript.SetTarget(dialogue.speaker);

        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }

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
            if (dialogueSound != null)
            {
                dialogueSound.Stop();
            }
        }
        else
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            if (dialogueSound != null)
            {
                dialogueSound.Play();
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
        if (dialogueSound != null)
        {
            dialogueSound.Stop();
        }
    }

    public void EndDialogue()
    {
        if (dialogueSound != null)
        {
            dialogueSound.Stop();
        }
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
            cameraScript.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        finishedDialogue = true;
    }
}
