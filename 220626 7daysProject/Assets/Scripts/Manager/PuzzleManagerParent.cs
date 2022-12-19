using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerParent : MonoBehaviour
{
    [SerializeField]
    GameObject prefabsCanvas;
    [SerializeField]
    GameObject dialoguePrefab;
    [SerializeField]
    GameObject questPrefab;
    [SerializeField]
    Text questNameText;
    [SerializeField]
    Text questGoalText;

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
