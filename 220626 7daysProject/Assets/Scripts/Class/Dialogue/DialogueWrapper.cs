using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueWrapper
{
    public Dialogue[] dialogueArray;

    public DialogueWrapper()
    {
        
    }

    public void Parse()
    {
        for (int i = 0; i < dialogueArray.Length; i++)
            dialogueArray[i].Parse();
    }
}
