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

        SetMirPosition();
    }

    void SetMirPosition()
    {
        if (GameManager.singleton.questSaveData.isNowQuestClear == true)
        {
            //Ʃ�� Ŭ������ ���
            if (GameManager.singleton.saveData.isPuzzleClear[0])
                mir.transform.position = tutorialPortalTransform.position;
            else if(GameManager.singleton.saveData.isPuzzleClear[1])
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

    //�� ���� �ʱ�ȭ ��ų �� ������
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
                break;
            case Actions.FindSound:
                Debug.Log("FindSound");
                break;
            case Actions.OldManAppear:
                Debug.Log("OldManAppear");
                break;
            case Actions.GetQuest1:
                Debug.Log("GetQuest1");
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

        //�̸� �������� ���߸鼭 �ݶ��̴��� ���� ���·� �����Ǵ� ���� ����
        if (mirMoveData.mirAttackRange.activeSelf == true)
            mirMoveData.mirAttackRange.SetActive(false);
    }

    //Action Functions
    IEnumerator GoToOldMan()
    {
        SetMainCamera(directingCamera);
        Transform dest = oldMan.transform;
        //�� �ݰ� �̸� ����
        mir.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        SetMainCamera(playerCamera);
        mir.SetActive(true);
        ControlMirAction(true);
        yield return new WaitForSeconds(0.3f);
        //�̸��� ���ο��� ������ ��
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
        getQuestSaveData.SetGoalText("������� �� ��������", "�� : ");

        questPrefab.SetActive(true);
        questNameText.text = getQuestSaveData.questNameText;
        questGoalText.text = getQuestSaveData.questGoalText;

        GameManager.singleton.questSaveData = getQuestSaveData;

        GameManager.singleton.SaveNowData();
        isActionPlaying = false;
    }

    IEnumerator OpenSound()
    {
        //���� ������ �Ҹ��� �鸲

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
        //�̸��� ������ �Բ� ����Ŀ���� ��
        StartCoroutine(oldManScript.MoveToDest(dest, 2f));
        StartCoroutine(mirMoveData.MoveToDest(dest, 2f));

        yield return new WaitForSeconds(2f);
        dialogueManager.ScreenTouchEvent();
    }
}
