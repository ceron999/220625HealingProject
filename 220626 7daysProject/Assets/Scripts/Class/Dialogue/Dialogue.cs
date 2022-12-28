using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Types
{ 
    Null, Dialog
}

public enum Actions
{
    Null, GoToOldMan, GetTutorialQuest, OpenSound
}

[System.Serializable]
public class Dialogue
{
    //대화 관련 입력값들
    public string typeString;
    public string actionString;
    public string characterName;
    public string dialogue;

    public bool getQuest;
    public bool clearQuest;

    public Types type;
    public Actions action;

    public Dialogue()
    {
        typeString = "";
        actionString = "";
        characterName = "";
        dialogue = "";

        getQuest = false;
        clearQuest = false;

        type = Types.Null;
        action = Actions.Null;
    }

    public void Parse()
    {
        if(typeString != "")
            type = (Types)Enum.Parse(typeof(Types), typeString);
        if (actionString != "")
            action = (Actions)Enum.Parse(typeof(Actions), actionString);
    }
}
