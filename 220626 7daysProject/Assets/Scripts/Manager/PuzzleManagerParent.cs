using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerParent : MonoBehaviour
{
    [SerializeField]
    protected DialogueManager dialogueManager;
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

    public string getColObjectName;

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
