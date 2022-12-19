using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    JsonManager jsonManager;
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Camera directingCamera;
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject questPrefab;
    [SerializeField]
    Text questNameText;
    [SerializeField]
    Text questGoalText;

    [SerializeField] 
    GameObject mir;
    [SerializeField]
    GameObject oldMan;
    MirMove mirMoveData;

    void Start()
    {
        jsonManager = new JsonManager();
        mirMoveData = mir.GetComponent<MirMove>();
    }

    public void SetAction(Dialogue nowDialogue)
    {
        switch (nowDialogue.action)
        {
            case Actions.GoToOldMan:
                Debug.Log("GotoOldMan");
                StartCoroutine(GoToOldMan());
                break;
            case Actions.GetTutorialQuest:
                Debug.Log("GetTutorialQuest");
                SetTutorialQuest();
                break;
        }
    }
    public bool GetMirIsAction()
    {
        return mirMoveData.isAction;
    }

    public void ControlMirAction(bool active)
    {
        mirMoveData.isAction = active;
    }

    //Action Functions
    IEnumerator GoToOldMan()
    {
        SetMainCamera(directingCamera);
        Transform dest = oldMan.transform;
        //문 닫고 미르 등장
        mir.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        SetMainCamera(playerCamera);
        mir.SetActive(true);
        ControlMirAction(true);
        yield return new WaitForSeconds(0.3f);
        //미르가 노인에게 가까이 감
        StartCoroutine(mirMoveData.MoveToDest(dest, 0.7f));

        yield return new WaitForSeconds(1.5f);
        dialogueManager.DialoguePrefabToggle(true);
        dialogueManager.ScreenTouchEvent();
        dialogueManager.isDialogueStart = true;
    }

    void SetMainCamera(Camera getMainCamera)
    {
        if(getMainCamera == playerCamera)
        {
            playerCamera.enabled = true;
            directingCamera.enabled = false;
            mainCamera = playerCamera;
        }
        else
        {
            playerCamera.enabled = false;
            directingCamera.enabled = true;
            mainCamera = directingCamera;
        }
    }

    void SetTutorialQuest()
    {
        SaveData getSaveData = GameManager.singleton.saveData;
        QuestSaveData getQuestSaveData = GameManager.singleton.questSaveData;
        getSaveData.isPuzzleStart[0] = true;
        getQuestSaveData.questNameText = "촌장님의 술 가져오기";
        getQuestSaveData.questGoal = 1;
        getQuestSaveData.questGoalText = "술 : " + getQuestSaveData.nowGetCount + "/" + getQuestSaveData.questGoal;

        questPrefab.SetActive(true);
        questNameText.text = getQuestSaveData.questNameText;
        questGoalText.text = getQuestSaveData.questGoalText;

        jsonManager.SaveJson(getQuestSaveData, "questSaveData");
        jsonManager.SaveJson(getSaveData, "saveData");
    }
}
