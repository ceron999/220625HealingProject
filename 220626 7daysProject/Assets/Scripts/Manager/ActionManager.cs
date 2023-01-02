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

    public bool isActionPlaying = false;

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
            case Actions.OpenSound:
                Debug.Log("OpenSound");
                StartCoroutine(OpenSound());
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

        //미르 움직임이 멈추면서 콜라이더가 켜진 상태로 유지되는 현상 수정
        if (mirMoveData.mirAttackRange.activeSelf == true)
            mirMoveData.mirAttackRange.SetActive(false);
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
        isActionPlaying = false;
    }

    public void SetPlayerCamera()
    {
        playerCamera.enabled = true;
        directingCamera.enabled = false;
        mainCamera = playerCamera;
    }

    void SetMainCamera(Camera getMainCamera)
    {
        if(getMainCamera == playerCamera)
        {
            playerCamera.gameObject.SetActive(true);
            playerCamera.enabled = true;
            directingCamera.enabled = false;
            directingCamera.gameObject.SetActive(false);
            mainCamera = playerCamera;
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
            playerCamera.enabled = false;
            directingCamera.gameObject.SetActive(true);
            directingCamera.enabled = true;
            mainCamera = directingCamera;
        }
    }

    void SetTutorialQuest()
    {
        QuestSaveData getQuestSaveData = GameManager.singleton.questSaveData;
        GameManager.singleton.saveData.isPuzzleStart[0] = true;

        getQuestSaveData.questGoal = 1;
        getQuestSaveData.SetGoalText("촌장님의 술 가져오기", "술 : ");

        questPrefab.SetActive(true);
        questNameText.text = getQuestSaveData.questNameText;
        questGoalText.text = getQuestSaveData.questGoalText;

        GameManager.singleton.questSaveData = getQuestSaveData;

        GameManager.singleton.SaveNowData();
        isActionPlaying = false;
    }

    IEnumerator OpenSound()
    {
        //뭔가 열리는 소리가 들림

        yield return new WaitForSeconds(1);
        dialogueManager.ScreenTouchEvent();
        isActionPlaying = false;
    }
}
