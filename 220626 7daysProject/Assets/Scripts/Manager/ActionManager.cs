using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    JsonManager jsonManager;
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    Transform tutorialPortalTransform;
    [SerializeField]
    Transform portalTransform;

    [SerializeField]
    GameObject tutorialPortal;
    [SerializeField]
    GameObject storagePortal;
    [SerializeField]
    GameObject portal;

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

    [SerializeField]
    GameObject bakery;

    public bool isActionPlaying = false;

    void Start()
    {
        jsonManager = new JsonManager();
        mirMoveData = mir.GetComponent<MirMove>();

        SetPortalActive();
        SetQuestPrefab();
        SetMirPosition();
    }
    void SetPortalActive()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "VillageScene")
        {
            if (!GameManager.singleton.saveData.isPuzzleClear[0])
            {
                tutorialPortal.SetActive(true);
            }
            else if (!GameManager.singleton.saveData.isPuzzleClear[1])
            {
                storagePortal.SetActive(true);
            }
            else if (!GameManager.singleton.saveData.isPuzzleClear[2])
            {
                portal.SetActive(true);
            }
        }
    }

    //로드할 때 퀘스트 다시 보여주기 위해
    void SetQuestPrefab()
    {
        int puzzleSize = GameManager.singleton.saveData.isPuzzleStart.Length;
        for (int i = 0; i < puzzleSize; i++)
            if (GameManager.singleton.saveData.isPuzzleStart[i])
            {
                questPrefab.SetActive(true);

                questNameText.text = GameManager.singleton.questSaveData.questNameText;
                questGoalText.text = GameManager.singleton.questSaveData.questGoalText;
                break;
            }
    }

    //로드나 돌아올 때 위치 설정
    void SetMirPosition()
    {
        if (GameManager.singleton.questSaveData.isNowQuestClear == true)
        {
            //튜토 클리어할 경우
            if (GameManager.singleton.saveData.isPuzzleClear[0])
                mir.transform.position = tutorialPortalTransform.position;
            else if (GameManager.singleton.saveData.isPuzzleClear[1])
            {

            }
            else if (GameManager.singleton.saveData.isPuzzleClear[2])
            {

            }
            else if (GameManager.singleton.saveData.isPuzzleClear[3])
            {

            }
        }
    }

    //퀘 깨고 초기화 시킬 때 쓰려고
    public void ClearQuestPrefab()
    {
        questNameText.text = "";
        questGoalText.text = "";
        questPrefab.SetActive(false);
    }

    public void SetAction(Dialogue nowDialogue)
    {
        switch (nowDialogue.action)
        {
            //tutorial Actions
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

            //quest1 Actions
            case Actions.MoveToBakery:
                Debug.Log("MoveToBakery");
                StartCoroutine(MoveToBakery());
                break;
            case Actions.OldManDisappear:
                Debug.Log("OldManDisappear");
                StartCoroutine(OldManDisappear());
                break;
            case Actions.GetQuest1:
                Debug.Log("GetQuest1");
                GetQuest1();
                break;
        }
    }
    public bool GetMirIsAction()
    {
        return mirMoveData.isAction;
    }

    public void ControlMirAction(bool active)
    {
        if (mirMoveData.moveDirection != 0)
            mirMoveData.moveDirection = 0;

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
        if (getMainCamera == playerCamera)
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
        tutorialPortal.SetActive(true);
    }

    IEnumerator OpenSound()
    {
        //뭔가 열리는 소리가 들림

        yield return new WaitForSeconds(1);
        dialogueManager.ScreenTouchEvent();
        isActionPlaying = false;
    }

    IEnumerator MoveToBakery()
    {
        Transform dest = bakery.transform;
        dest.position -= -new Vector3(1, 0, 0);
        ControlMirAction(true);
        OldMan oldManScript = oldMan.GetComponent<OldMan>();
        //미르와 노인이 함께 베이커리로 감
        StartCoroutine(oldManScript.MoveToDest(dest, 2f));
        StartCoroutine(mirMoveData.MoveToDest(dest, 2f));

        yield return new WaitForSeconds(2f);
        isActionPlaying = false;
        dialogueManager.ScreenTouchEvent();
    }

    IEnumerator OldManDisappear()
    {
        oldMan.SetActive(false);
        yield return new WaitForSeconds(1);

        //우당탕탕 소리 남
        yield return new WaitForSeconds(2);

        oldMan.SetActive(true);
        yield return new WaitForSeconds(1);

        isActionPlaying = false;
        dialogueManager.ScreenTouchEvent();
    }

    void GetQuest1()
    {
        QuestSaveData getQuestSaveData = GameManager.singleton.questSaveData;
        GameManager.singleton.saveData.isPuzzleStart[1] = true;

        getQuestSaveData.questGoal = 1;
        getQuestSaveData.SetGoalText("촌장님의 창고로 가서 블루베리 가져오기", "블루베리 : ");

        questPrefab.SetActive(true);
        questNameText.text = getQuestSaveData.questNameText;
        questGoalText.text = getQuestSaveData.questGoalText;

        GameManager.singleton.questSaveData = getQuestSaveData;

        GameManager.singleton.SaveNowData();
        isActionPlaying = false;
        storagePortal.SetActive(true);
    }
}
