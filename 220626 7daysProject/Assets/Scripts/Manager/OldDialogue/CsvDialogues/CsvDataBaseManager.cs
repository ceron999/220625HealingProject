using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//해당 씬에서 파싱한 특정 데이터를 원할 때,
//DialogueParser를 통해 csv파일을 파싱하고, 해당 데이터를 dialogueDictionary에 저장하는 매니저.

public class CsvDataBaseManager : MonoBehaviour
{
    public static CsvDataBaseManager dataBaseManager;

    [SerializeField] string csvFileName;

    public Dictionary<int, CsvDialogue> dialogueDictionary = new Dictionary<int, CsvDialogue>();

    public static bool isFinished = false;

    void Awake()
    {
        //매니저가 존재하지 않으면 현재 오브젝트를 싱글톤으로 생성합니다.
        if (dataBaseManager == null)
        {
            dataBaseManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDialogueDictionary(string getCsvFileName)
    {
        //parser를 불러와서 파싱한 dialogueList를 가져온다.
        CsvDialogueParser theParser = GetComponent<CsvDialogueParser>();
        CsvDialogue[] dialogues = theParser.Parse(getCsvFileName);

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDictionary.Add(i + 1, dialogues[i]);
        }
        isFinished = true;
    }

    public void ResetDialogueDictionary()
    {
        for(int i=0; i<dialogueDictionary.Count; i++)
        {
            dialogueDictionary.Remove(i);
        }
        isFinished = false;
    }

    public CsvDialogue[] GetDialogue(int startNum, int endNum)
    {
        List<CsvDialogue> dialogueList = new List<CsvDialogue>();

        for(int i =0; i<endNum - startNum; i++)
        {
            dialogueList.Add(dialogueDictionary[startNum + i]);
        }

        return dialogueList.ToArray();
    }
}
