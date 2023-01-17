using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerParent : MonoBehaviour
{
    [SerializeField]
    protected DialogueManager dialogueManager;
    [SerializeField]
    protected ActionManager actionManager;
    [SerializeField] 
    protected PortalManager portalManager;
    [SerializeField]
    protected GameObject prefabsCanvas;
    [SerializeField]
    protected GameObject dialoguePrefab;
    [SerializeField]
    protected GameObject questPrefab;
    [SerializeField]
    protected Text questNameText;
    [SerializeField]
    protected Text questGoalText;

    public string getColObjectName; //공격 콜라이더에 충돌한 객체 이름

    void Start()
    {
        int puzzleSize = GameManager.singleton.saveData.isPuzzleStart.Length;
        for(int i =0; i < puzzleSize; i++)
            if(GameManager.singleton.saveData.isPuzzleStart[i])
            {
                prefabsCanvas.SetActive(true);
                questPrefab.SetActive(true);

                questNameText.text = GameManager.singleton.questSaveData.questNameText;
                questGoalText.text = GameManager.singleton.questSaveData.questGoalText;
                break;
            }
    }
}
