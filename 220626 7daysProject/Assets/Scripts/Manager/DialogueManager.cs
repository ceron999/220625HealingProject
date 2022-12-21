using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    JsonManager jsonManager;
    [SerializeField]
    ActionManager actionManager;

    [SerializeField] 
    DialogueWrapper dialogueWrapper;
    public string dialogueWrapperName;

    [SerializeField] 
    GameObject dialoguePrefab;
    [SerializeField] 
    Text characterNameText;
    [SerializeField] 
    Text dialogueText;

    [SerializeField]
    GameObject screenTouchCanvas;

    [SerializeField]
    int nowDialogueIndex;
    bool isSkip = false;
    public bool isDialogueStart = false;
    bool isDialoguePrinting = false;
    bool isDialogueEnd = false;
    Coroutine nowCoroutine;


    void Start()
    {
        jsonManager = new JsonManager();
        nowDialogueIndex = 0;

        if (!GameManager.singleton.saveData.isFirstStart)
        {
            GameManager.singleton.saveData.isFirstStart = true;
            jsonManager.SaveJson(GameManager.singleton.saveData, "saveData");

            dialogueWrapperName = GameManager.singleton.setDialogueName;
            dialogueWrapper = jsonManager.ResourceDataLoad<DialogueWrapper>(dialogueWrapperName);
            dialogueWrapper.Parse();

            ScreenTouchEvent();
        }
    }

    //for Developer
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isDialogueStart)
        {
            StartCoroutine(SkipDialogue());
        }
    }

    public void LoadDialogue(string getDialogueName)
    {
        if (dialoguePrefab.activeSelf == false)
        {
            dialogueWrapperName = getDialogueName;
            dialogueWrapper = jsonManager.ResourceDataLoad<DialogueWrapper>(dialogueWrapperName);
            dialogueWrapper.Parse();
            dialoguePrefab.SetActive(true);
            SetScreenTouchCanvas(true);
            isDialogueStart = true;
            ScreenTouchEvent();
        }
    }

    void SetScreenTouchCanvas(bool active)
    {
        screenTouchCanvas.SetActive(active);
    }

    public void ScreenTouchEvent()
    {
        if (!isDialogueEnd && dialogueWrapper != null)
            PrintDialogue();
    }

    void PrintDialogue()
    {
        if (!isDialoguePrinting)
        {
            if (nowDialogueIndex == dialogueWrapper.dialogueArray.Length)
            {
                Debug.Log("DialogueEnd");
                SetScreenTouchCanvas(false);
                DialoguePrefabToggle(false);
                isDialogueEnd = true;
                isDialogueStart = false;

                if (actionManager.GetMirIsAction()) 
                    actionManager.ControlMirAction(false);

                nowDialogueIndex = 0;
                dialogueWrapper = null;
                dialogueWrapperName = "";
            }

            if (nowDialogueIndex < dialogueWrapper.dialogueArray.Length)
            {
                Dialogue nowDialogue = dialogueWrapper.dialogueArray[nowDialogueIndex];
                Debug.Log("PrintDialog: " + nowDialogueIndex + "/" + dialogueWrapper.dialogueArray.Length);

                //타입에 따라 대화창을 껏다 킴.
                if (nowDialogue.type == Types.Null)
                {
                    SetScreenTouchCanvas(false);
                    DialoguePrefabToggle(false);
                    if(nowDialogue.action != Actions.Null)
                        actionManager.SetAction(nowDialogue);

                    nowDialogueIndex++;
                    if (nowDialogueIndex == dialogueWrapper.dialogueArray.Length)
                        PrintDialogue();
                    return;
                }
                else if (nowDialogue.type == Types.Dialog)
                {
                    DialoguePrefabToggle(true);
                    SetScreenTouchCanvas(true);
                }
                else Debug.Log("nowDialogue type Error");

                //action이 존재하면 실행
                if (nowDialogue.action != Actions.Null)
                    actionManager.SetAction(nowDialogue);

                if (!isDialogueEnd && !isDialoguePrinting)
                {
                    dialogueText.text = "";
                    nowCoroutine = StartCoroutine(PrintDialogueText());
                }

                nowDialogueIndex++;
            }
        }
        else if (isDialoguePrinting)
        {
            SpreadDialogue(dialogueWrapper.dialogueArray[nowDialogueIndex - 1]);
        }
    }

    IEnumerator PrintDialogueText()
    {
        isDialoguePrinting = true;
        Dialogue nowDialogue = dialogueWrapper.dialogueArray[nowDialogueIndex];

        characterNameText.text = nowDialogue.characterName;

        for(int i = 0; i< nowDialogue.dialogue.Length; i++)
        {
            dialogueText.text += nowDialogue.dialogue[i];
            yield return new WaitForSeconds(0.07f);
        }

        isDialoguePrinting = false;
    }

    void SpreadDialogue(Dialogue nowDialogue)
    {
        StopCoroutine(nowCoroutine);
        characterNameText.text = nowDialogue.characterName;

        dialogueText.text = "";
        dialogueText.text = nowDialogue.dialogue;

        isDialoguePrinting = false;
    }    

    IEnumerator SkipDialogue()
    {
        if (dialogueWrapper != null)
        {
            if (!isSkip)
            {
                if (dialogueWrapper.dialogueArray[nowDialogueIndex - 1].action != Actions.Null)
                    actionManager.SetAction(dialogueWrapper.dialogueArray[nowDialogueIndex - 1]);

                if (nowDialogueIndex >= dialogueWrapper.dialogueArray.Length)
                {
                    Debug.Log("Skip Final : " + nowDialogueIndex +"/" + dialogueWrapper.dialogueArray.Length);
                    SetScreenTouchCanvas(false);
                    dialoguePrefab.SetActive(false);
                    isDialogueEnd = true;
                    isDialogueStart = false;
                    actionManager.ControlMirAction(false);

                    nowDialogueIndex = 0;
                    dialogueWrapper = null;
                    dialogueWrapperName = "";
                }

                if (nowDialogueIndex < dialogueWrapper.dialogueArray.Length)
                {
                    isSkip = true;
                    isDialoguePrinting = false;
                    StopCoroutine(nowCoroutine);
                    Debug.Log(nowDialogueIndex + "/" + dialogueWrapper.dialogueArray.Length);
                    characterNameText.text = dialogueWrapper.dialogueArray[nowDialogueIndex - 1].characterName;
                    dialogueText.text = dialogueWrapper.dialogueArray[nowDialogueIndex - 1].dialogue;


                    yield return new WaitForSeconds(0.1f);
                    nowDialogueIndex++;
                    isSkip = false;
                }
            }
        }
    }

    public void DialoguePrefabToggle(bool active)
    {
        if(active)
        {
            //페이드 효과
            dialoguePrefab.SetActive(true);
        }
        else
        {
            //페이드 효과
            dialoguePrefab.SetActive(false);
        }
    }
}
