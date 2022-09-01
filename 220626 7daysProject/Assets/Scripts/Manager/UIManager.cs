using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    [SerializeField] GameObject talkUI;
    [SerializeField] GameObject leftBTN;
    [SerializeField] GameObject rightBTN;
    [SerializeField] GameObject attackBTN;
    [SerializeField] GameObject JumpBTN;

    public bool isTalk;

    void Awake()
    {
        //�Ŵ����� �������� ������ ���� ������Ʈ�� �̱������� �����մϴ�.
        if (uiManager == null)
        {
            uiManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeUIActive()
    {
        if(isTalk)
        {
            talkUI.SetActive(true);
            leftBTN.SetActive(false);
            rightBTN.SetActive(false);
            attackBTN.SetActive(false);
            JumpBTN.SetActive(false);
        }
        else
        {
            talkUI.SetActive(false);
            leftBTN.SetActive(true);
            rightBTN.SetActive(true);
            attackBTN.SetActive(true);
            JumpBTN.SetActive(true);
        }
    }
}