using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool isFirstStart;
    public bool[] isPuzzleClear;

    public SaveData()
    {
        isFirstStart = false;
        isPuzzleClear = new bool[4];

        for(int i =0; i< 3; i++)
        {
            isPuzzleClear[i] = false;
        }
    }
}
