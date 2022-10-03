using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialScenePortal;

    //��Ż�� Ż �� �ִ��� �˻��մϴ�.
    public bool CanTakePortal(string getPortalName)
    {
        Debug.Log(getPortalName);
        switch (getPortalName)
        {
            case "TutorialScenePortal":
                return true;

                //�� �� ���뼺 �ְ� ����ϱ� ���ؼ��� �� �κ� �����ؾ���@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            case "Village1ScenePortal":
                if (GameManager.gameManager.isTutorialGoalClear)
                {
                    GameManager.gameManager.isTutorialClear = true;
                    return true;
                }
                break;

        }
        return false;
    }

    //Ʃ�丮�� ��Ż�� Ȱ��ȭ ���θ� �����մϴ�. 
    void SetTutorialPortal()
    {
        if (GameManager.gameManager.isTutorialClear)
        {
            tutorialScenePortal.SetActive(false);
            Debug.Log("Ʃ�� Ŭ����");
        }
    }

    private void Start()
    {
        SetTutorialPortal();
    }
}
