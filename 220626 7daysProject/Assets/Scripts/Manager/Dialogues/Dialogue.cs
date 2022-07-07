using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string contexts;
}

public class DialogueEvent
{
    public Vector2 line;    //대사를 x줄~y줄 까지 받아오는 변수
    public Dialogue[] dialogues;    //여러 캐릭터들의 대화가 포함되어야 하므로 배열화
}