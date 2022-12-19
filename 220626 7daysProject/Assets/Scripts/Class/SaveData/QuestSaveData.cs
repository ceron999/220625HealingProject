using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    public string questNameText;
    public string questGoalText;

    public int nowGetCount;
    public int questGoal;
    public bool isNowQuestClear;
    public QuestSaveData()
    {
        questNameText = "";
        questGoalText = "";

        nowGetCount = 0;
        questGoal = 0;
        isNowQuestClear = false;
    }
}
