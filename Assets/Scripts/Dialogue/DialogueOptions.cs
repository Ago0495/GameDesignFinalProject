using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOptions : MonoBehaviour
{
    public string entityName;
    public Dialogue[] DialogueList;

    void Start()
    {
        foreach (var dialogue in DialogueList)
        {
            dialogue.SetName(entityName);
        }
        Debug.Log("On Start");
    }
}
