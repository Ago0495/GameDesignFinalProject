using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private string name;
    [SerializeField] public Transform speaker;
    [SerializeField] public AudioClip dialogueAudio;
    [TextArea(3,10)]
    public string[] sentences;

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}
