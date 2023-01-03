using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Image drinkImage;


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
        if (isKeyGet)
        {
            StartCoroutine(OpenFakeChestCoroutine());
        }
        else
        {
            dialogueManager.LoadDialogue("TutorialFakeChest");
        }
    }

    IEnumerator OpenFakeChestCoroutine()
    {
        actionManager.ControlMirAction(true);
        
        //Chest open
        fakeChestAnimator = tutorialFakeChest.GetComponent<Animator>();
        fakeChestAnimator.SetBool("isOpen", true);
        isFakeChestOpen = true;

        fakeTile.SetActive(false);
        tutorialRealChest.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        dialogueManager.LoadDialogue("TutorialFakeChestOpen");
    }

    void OpenRealChest()
    {
        if(isKeyGet && isFakeChestOpen)
        {
            actionManager.ControlMirAction(true);
            //Chest open
            realChestAnimator = tutorialRealChest.GetComponent<Animator>();
            realChestAnimator.SetBool("isOpen", true);
            isRealChestActive = true;

            //술 얻음
            StartCoroutine(GetDrink());

            GameManager.singleton.questSaveData.nowGetCount++;
            GameManager.singleton.questSaveData.isNowQuestClear = true;
            GameManager.singleton.questSaveData.SetGoalText("촌장님의 술 가져오기", "술 : ");
            GameManager.singleton.saveData.isPuzzleClear[0] = true;

            questGoalText.text = GameManager.singleton.questSaveData.questGoalText;
        }
    }

    IEnumerator FadeDrinkImage()
    {
        drinkImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        float fadeFloat = 0;

        while (fadeFloat <= 1)
        {
            fadeFloat += 0.05f;
            drinkImage.color = new Color(255, 255, 255, fadeFloat);
            yield return new WaitForSeconds(0.1f);
        }

        //임시로 만듬
        while (Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        while (fadeFloat >= 0)
        {
            fadeFloat -= 0.05f;
            drinkImage.color = new Color(255, 255, 255, fadeFloat);
            yield return new WaitForSeconds(0.1f);
        }

        drinkImage.gameObject.SetActive(false);
    }

    IEnumerator GetDrink()
    {
        yield return StartCoroutine(FadeDrinkImage());

        dialogueManager.LoadDialogue("TutorialRealChestOpen");
    }
}
