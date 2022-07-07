using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager dataBaseManager;

    [SerializeField] string csvFileName;
    public int sceneNum;
    
    Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();
    
    public static bool isFinished = false;

    void Awake()
    {
        //매니저가 존재하지 않으면 현재 오브젝트를 싱글톤으로 생성합니다.
        if (dataBaseManager == null)
        {
            dataBaseManager = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    private void Start()
    {
        //parser를 불러와서 파싱한 dialogueList를 가져온다.
        DialogueParser theParser = GetComponent<DialogueParser>(); 
        Dialogue[] dialogues = theParser.Parse(csvFileName);

        for(int i =1; i<dialogues.Length;)
        {
            dialogueDictionary.Add(sceneNum, dialogues[i]);
        }
        isFinished = true;
    }
}
