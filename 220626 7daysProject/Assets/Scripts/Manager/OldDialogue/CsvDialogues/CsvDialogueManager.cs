using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//gameManager에서 대화를 원할 떄 나타나서 대화를 출력하는 매니저

public class CsvDialogueManager : MonoBehaviour
{
    public GameObject talkUI;
    public Text characterNameText;
    public Text talkText;

    public Dictionary<int, CsvDialogue> dialogueDict = new Dictionary<int, CsvDialogue>();
    public string csvFileName;
    public string nextSceneName;


    private void Start()
    {
        SetDictionary();
        StartCoroutine(PrintDialogue());
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
            SpreadText();
    }

    public void SetDictionary()
    {
        CsvDataBaseManager.dataBaseManager.SetDialogueDictionary(csvFileName);
        dialogueDict = CsvDataBaseManager.dataBaseManager.dialogueDictionary;
    }

    public IEnumerator PrintDialogue()
    {
        int dialogueSize;

        dialogueSize = CsvDataBaseManager.dataBaseManager.dialogueDictionary.Count;

        for (int i = 1; i <= dialogueSize; i++)
        {
            yield return StartCoroutine(TypingContext(i));
        }

        Debug.Log(nextSceneName);
        //모든 대사를 출력하면 다음 씬으로 넘어가요.
        if(nextSceneName == "Village1Scene")
            SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypingContext(int contextIdx)
    {
        //한 캐릭터가 말하는 대사의 개수 ex) 할아버지가 연속으로 7개의 대사를 하면 contextSize = 7;
        int contextSize;
        contextSize = dialogueDict[contextIdx].contexts.Length;

        characterNameText.text = dialogueDict[contextIdx].name;

        for(int contextCol=0; contextCol< contextSize; contextCol++)
        {
            talkText.text = "";
            
            //출력하는 대사의 길이를 나타내는 값
            int getCount = dialogueDict[contextIdx].contexts[contextCol].Length;
            
            for (int contextRow=0; contextRow < getCount; contextRow++)
            {
                if (dialogueDict[contextIdx].contexts[contextCol][contextRow] != '^')
                    //한글자씩 출력합니다잉
                    talkText.text += dialogueDict[contextIdx].contexts[contextCol][contextRow];
                else
                    talkText.text += ',';
                

                //만일 터치하면 대사를 한번에 보여줍니다잉
                if (Input.GetMouseButton(0))
                {
                    talkText.text = "";
                    for(int i=0; i< dialogueDict[contextIdx].contexts[contextCol].Length; i++)
                    {
                        if (dialogueDict[contextIdx].contexts[contextCol][i] != '^')
                            //한글자씩 출력합니다잉
                            talkText.text += dialogueDict[contextIdx].contexts[contextCol][i];
                        else
                            talkText.text += ',';
                    }
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            //스크립트가 전부 끝나면 터치할 때까지 대기합니다.
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitForSeconds(0.3f);
            ///코드 설명
            ///문제점: 
            ///getmousebuttondown(0)가 실행되면 다음 문장의 글자를 출력하는 if(input.getmousebutton(0))에서도
            ///참으로 인식되어 다음 문장이 한글자씩 출력되지 않고 한번에 출력이 되었음.
            ///해결 방식:
            ///문제는 내가 클릭을 누르고 떼는 시간동안 다음 코드가 진행되어 발생하므로
            ///다음 코드를 지연시켜 클릭이 완전히 끝난 다음에 실행되도록 waitForSeconds를 추가 삽입.
        }
    }

    void SpreadText()
    {

    }
}