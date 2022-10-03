using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    PortalManager portalManager;

    public bool isTutorialGoalClear = false;
    public bool isTutorialClear = false;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
