using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneBTN : MonoBehaviour
{
    public void GameStartBtn()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void GameLoadBtn()
    {
        //자동 저장된 시점으로 이동
    }

    public void GameExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
