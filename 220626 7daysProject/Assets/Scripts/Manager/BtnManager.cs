using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    //Main Scene BTN 클릭할 경우
    public void ClickStartBTN()
    {
        GameManager.singleton.SetSaveDataClear();
        GameManager.singleton.setDialogueName = "TutorialStart";

        SceneManager.LoadScene("VillageScene");
    }

    public void ClickLoadBTN()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void ClickExitBTN()
    {
        Application.Quit();
    }
}