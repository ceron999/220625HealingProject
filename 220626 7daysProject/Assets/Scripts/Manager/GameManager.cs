using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    JsonManager jsonManager;
    PortalManager portalManager;

    public SaveData saveData;

    public string setDialogueName;
    //temp
    public bool isTutorialGoalClear = false;
    public bool isTutorialClear = false;

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

        saveData = jsonManager.LoadSaveData();
    }

    //세이브데이터 날리기
    public void SetSaveDataClear()
    {
        saveData = new SaveData();
        jsonManager.SaveJson(saveData, "saveData");
    }
}
