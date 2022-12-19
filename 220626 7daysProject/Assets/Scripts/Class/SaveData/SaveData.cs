using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool isFirstStart;
    public bool[] isPuzzleStart;
    public bool[] isPuzzleClear;

    public SaveData()
    {
        isFirstStart = false;
        isPuzzleStart = new bool[4];
        isPuzzleClear = new bool[4];
        for (int i = 0; i < 3; i++)
        {
            isPuzzleStart[i] = false;
        }
        for (int i =0; i< 3; i++)
        {
            isPuzzleClear[i] = false;
        }
    }
}
