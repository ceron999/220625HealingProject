using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//gameManager에서 대화를 원할 떄 나타나서 대화를 출력하는 매니저

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;

    public static bool isTalking = false;

    public GameObject TalkUI;
    public Text characterNameText;
    public Text talkText;

    // Start is called before the first frame update
    void Awake()
    {
        if (dialogueManager == null)
            dialogueManager = this;
    }

    private void Update()
    {
        if (isTalking) 
            ;
    }

    /// 1. DataBaseManager에서 diaglogueDictonary를 받아온다.
    /// 2. dictonary에 있는 이름과 context를 출력한다.
    /// 3. 터치가 들어올 떄마다 다음 문장으로 넘어간다.
    /// 4. 모든 문장이 끝이 나면 talkUI를 setActive(false)시킨다.
}
