using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//gameManager에서 대화를 원할 떄 나타나서 대화를 출력하는 매니저

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;

    public static bool isTalking = false;
    bool isChangeChar = false;
    bool isContextFinish = false;

    public GameObject TalkUI;
    public Text characterNameText;
    public Text talkText;

    public Dictionary<int, Dialogue> dialogueDict = new Dictionary<int, Dialogue>();
    
    void Awake()
    {
        if (dialogueManager == null)
            dialogueManager = this;
    }

    private void Start()
    {
        SetDictionary();
        StartCoroutine(PrintDialogue());
    }

    public void SetDictionary()
    {
        DataBaseManager.dataBaseManager.SetDialogueDictionary("Scene0Intro");
        dialogueDict = DataBaseManager.dataBaseManager.dialogueDictionary;
    }

    public IEnumerator PrintDialogue()
    {
        int dialogueSize;

        dialogueSize = DataBaseManager.dataBaseManager.dialogueDictionary.Count;

        for (int i = 1; i <= dialogueSize; i++)
        {
            yield return StartCoroutine(TypingContext(i));
        }
    }

    IEnumerator TypingContext(int contextIdx)
    {
        //한 캐릭터가 말하는 대사의 개수 ex) 할아버지가 연속으로 7개의 대사를 하면 contextSize = 7;
        int contextSize;
        contextSize = dialogueDict[contextIdx].contexts.Length;

        characterNameText.text = dialogueDict[contextIdx].name;

        for(int i=0; i< contextSize; i++)
        {
            talkText.text = "";

            //출력하는 대사의 길이를 나타내는 값
            int getCount = dialogueDict[contextIdx].contexts[i].Length;

            
            for (int j=0; j< getCount; j++)
            {
                //한글자씩 출력합니다잉
                talkText.text += dialogueDict[contextIdx].contexts[i][j];

                //만일 터치하면 대사를 한번에 보여줍니다잉
                if (Input.GetMouseButtonDown(0))
                { 
                    talkText.text = dialogueDict[contextIdx].contexts[i];
                    isContextFinish = true;
                    break;
                }
                yield return new WaitForSeconds(0.1f);

                if(j == getCount - 1) isContextFinish = true;
            }

            //다음 대사로 넘아갑니다
            if (isContextFinish)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                isContextFinish = false;
            }
        }

    }
}
