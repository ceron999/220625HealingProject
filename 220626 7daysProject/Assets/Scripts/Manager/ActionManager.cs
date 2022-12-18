using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Camera directingCamera;
    [SerializeField]
    Camera mainCamera;

    [SerializeField] 
    GameObject mir;
    [SerializeField]
    GameObject oldMan;
    MirMove mirMoveData;

    void Start()
    {
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
        }
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
        yield return new WaitForSeconds(0.3f);
        //�̸��� ���ο��� ������ ��
        StartCoroutine(mirMoveData.MoveToDest(dest, 0.7f));

        yield return new WaitForSeconds(2f);
        dialogueManager.DialoguePrefabToggle(true);
        dialogueManager.ScreenTouchEvent();
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
}
