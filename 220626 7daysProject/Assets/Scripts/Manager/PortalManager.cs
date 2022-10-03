using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialScenePortal;

    //포탈을 탈 수 있는지 검사합니다.
    public bool CanTakePortal(string getPortalName)
    {
        Debug.Log(getPortalName);
        switch (getPortalName)
        {
            case "TutorialScenePortal":
                return true;

                //좀 더 범용성 있게 사용하기 위해서는 이 부분 변경해야함@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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

    //튜토리얼 포탈의 활성화 여부를 결정합니다. 
    void SetTutorialPortal()
    {
        if (GameManager.gameManager.isTutorialClear)
        {
            tutorialScenePortal.SetActive(false);
            Debug.Log("튜토 클리어");
        }
    }

    private void Start()
    {
        SetTutorialPortal();
    }
}
