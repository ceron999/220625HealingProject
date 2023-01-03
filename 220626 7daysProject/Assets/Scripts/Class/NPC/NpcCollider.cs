using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollider : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;
    [SerializeField]
    ActionManager actionManager;
    public bool isNpcColEnter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isNpcColEnter = true;
        if (dialogueManager.npcName == "Mir")
        {
            dialogueManager.npcName = this.name;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space) && !dialogueManager.isDialogueStart)
        {
            SetNpcDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null)
        {
            isNpcColEnter = false;
            if (dialogueManager.npcName != "")
                dialogueManager.npcName = "";
        }
    }

    void SetNpcDialogue()
    {
        //Æ©Åä¸®¾ó ³¡³µÀ» ¶§
        if (GameManager.singleton.saveData.isPuzzleClear[0] && GameManager.singleton.questSaveData.isNowQuestClear)
        {
            GameManager.singleton.ClearQuestData();
            actionManager.ClearQuestPrefab();
            dialogueManager.LoadDialogue("TutorialEnd");
        }
    }
}
