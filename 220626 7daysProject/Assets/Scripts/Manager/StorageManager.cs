using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : PuzzleManagerParent
{
    [SerializeField]
    GameObject key1;
    [SerializeField]
    GameObject key2;
    [SerializeField]
    GameObject key3;
    [SerializeField]
    GameObject chest;
    Animator chestAnimator;

    bool isGetKey;
    GameObject nowKey;
    bool isChestOpen;

    public void SetPuzzleProgress(string getColName)
    {
        switch (getColName)
        {
            case "Key1":
                GetKey1();
                break;
            case "Key2":
                GetKey2();
                break;
            case "Key3":
                GetKey3();
                break;
            case "Chest":
                OpenChest();
                break;
        }
    }

    void GetKey1()
    {
        if (!isChestOpen)
        {
            if (!isGetKey)
            {
                Debug.Log("getKey1");
                key1.SetActive(false);
                isGetKey = true;
                nowKey = key1;
            }
            else
            {
                dialogueManager.LoadDialogue("GetKey");
            }
        }
    }

    void GetKey2()
    {
        if (!isChestOpen)
        {
            if (!isGetKey)
            {
                Debug.Log("getKey2");
                key2.SetActive(false);
                isGetKey = true;
                nowKey = key2;
            }
            else
            {
                dialogueManager.LoadDialogue("GetKey");
            }
        }
    }

    void GetKey3()
    {
        if (!isChestOpen)
        {
            if (!isGetKey)
            {
                Debug.Log("getKey3");
                key3.SetActive(false);
                isGetKey = true;
                nowKey = key3;
            }
            else
            {
                dialogueManager.LoadDialogue("GetKey");
            }
        }
    }

    void OpenChest()
    {
        if (!isChestOpen)
        {
            if (!isGetKey)
            {
                dialogueManager.LoadDialogue("StorgaeChestNoKey");
            }
            else
            {
                if (nowKey == key3)
                {
                    StartCoroutine(OpenChestCoroutine());
                }
                else
                {
                    isGetKey = false;
                    nowKey = null;
                    dialogueManager.LoadDialogue("StorageChestNotCorrectKey");
                }
            }
        }
    }

    IEnumerator OpenChestCoroutine()
    {
        actionManager.ControlMirAction(true);
        isChestOpen = true;
        //Chest open
        chestAnimator = chest.GetComponent<Animator>();
        chestAnimator.SetBool("isOpen", true);
        portalManager.isStorageEnterPortalOpen = true;

        yield return new WaitForSeconds(1);

        dialogueManager.LoadDialogue("StorageChestCorrectKey");
    }
}
