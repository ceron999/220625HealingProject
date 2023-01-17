using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;

    //storage bool
    bool isStorageEnterPortalOpen;

    public void MoveScene(string getPortalName)
    {
        switch(getPortalName)
        {
            case "TutorialPortal":
                SceneManager.LoadScene("TutorialScene");
                break;
            case "TutorialEndPortal":
                UseTutorialEndPortal();
                break;
            case "StoragePortal":
                UseStoragePortal();
                break;
            case "StorageEnterPortal":
                UseStorageEnterPortal();
                break;
            case "StorageExitPortal":
                break;
        }
    }

    void UseTutorialEndPortal()
    {
        if (!GameManager.singleton.questSaveData.isNowQuestClear)
        {
            dialogueManager.LoadDialogue("TutorialPortal");
            Debug.Log("아직 게임 못깸");
        }
        else
        {
            GameManager.singleton.SaveNowData();
            SceneManager.LoadScene("VillageScene");
        }
    }

    void UseStoragePortal()
    {
        if (GameManager.singleton.saveData.isPuzzleStart[1])
        {
            SceneManager.LoadScene("StorageScene");
        }
    }

    void UseStorageEnterPortal()
    {
        if(!isStorageEnterPortalOpen)
        {
            dialogueManager.LoadDialogue("StorageEnterPortalNotOpen");
        }
        else
        {

        }
    }

    void UseStorageExitPortal()
    {

    }
}
