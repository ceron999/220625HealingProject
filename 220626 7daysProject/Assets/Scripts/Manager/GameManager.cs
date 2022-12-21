using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    JsonManager jsonManager;
    PortalManager portalManager;

    public QuestSaveData questSaveData;
    public SaveData saveData;

    public string setDialogueName;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        jsonManager = new JsonManager();

        questSaveData = jsonManager.LoadQuestSaveData();
        saveData = jsonManager.LoadSaveData();
    }

    //세이브데이터 날리기
    public void SetSaveDataClear()
    {
        saveData = new SaveData();
        questSaveData = new QuestSaveData();
        jsonManager.SaveJson(saveData, "saveData");
        jsonManager.SaveJson(questSaveData, "questSaveData");
    }

    public void SaveNowData()
    {
        jsonManager.SaveJson(saveData, "saveData");
        jsonManager.SaveJson(questSaveData, "questSaveData");
    }
}
