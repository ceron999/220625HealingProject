using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUI : MonoBehaviour
{
    [SerializeField] static TalkUI talkUI;
    private void Awake()
    {
        if(talkUI == null)
        {
            Debug.Log("1");
            talkUI = this;
            DontDestroyOnLoad(talkUI);
        }
        else
            Destroy(gameObject);
    }
}
