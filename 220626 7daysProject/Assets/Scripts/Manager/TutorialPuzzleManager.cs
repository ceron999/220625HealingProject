using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPuzzleManager : PuzzleManagerParent
{
    [SerializeField]
    GameObject tutorialKey;
    [SerializeField]
    GameObject tutorialFakeChest;
    [SerializeField]
    Animator fakeChestAnimator;
    [SerializeField]
    GameObject tutorialRealChest;
    [SerializeField]
    Animator realChestAnimator;
    [SerializeField]
    GameObject fakeTile;

    bool isKeyGet = false;
    bool isFakeChestOpen = false;
    bool isRealChestActive = false;
    bool isFakeTileActive = true;

    public void SetPuzzleProgress(string getColName)
    {
        switch(getColName)
        {
            case "TutorialKey":
                Debug.Log(getColName);
                GetTutorialKey();
                break;
            case "TutorialFakeChest":
                Debug.Log(getColName);
                OpenFakeChest();
                break;
            case "TutorialRealChest":
                Debug.Log(getColName);
                OpenRealChest();
                break;
        }
    }

    void GetTutorialKey()
    {
        tutorialKey.SetActive(false);
        isKeyGet = true;
    }

    void OpenFakeChest()
    {
        if(isKeyGet)
        {
            //Chest open
            fakeChestAnimator = tutorialFakeChest.GetComponent<Animator>();
            fakeChestAnimator.SetBool("isOpen", true);
            isFakeChestOpen = true;

            //가짜 타일 사라지기 전 혼잣말
            fakeTile.SetActive(false);
            tutorialRealChest.SetActive(true);
        }
        else
        {
            dialogueManager.LoadDialogue("TutorialFakeChest");
        }
    }

    void OpenRealChest()
    {
        if(isKeyGet && isFakeChestOpen)
        {
            //Chest open
            realChestAnimator = tutorialRealChest.GetComponent<Animator>();
            realChestAnimator.SetBool("isOpen", true);
            isRealChestActive = true;

            //술 얻음

            GameManager.singleton.questSaveData.nowGetCount++;
            GameManager.singleton.questSaveData.isNowQuestClear = true;
            GameManager.singleton.questSaveData.SetGoalText("촌장님의 술 가져오기", "술 : ");


            questGoalText.text = GameManager.singleton.questSaveData.questGoalText;
        }
    }
}
