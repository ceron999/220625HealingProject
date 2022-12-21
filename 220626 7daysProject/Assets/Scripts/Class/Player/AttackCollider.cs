using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField]
    TutorialPuzzleManager tutorialPuzzleManager;

    public string colObjectName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            colObjectName = collision.name;
            if (tutorialPuzzleManager != null)
            {
                tutorialPuzzleManager.getColObjectName = colObjectName;
                tutorialPuzzleManager.SetPuzzleProgress(colObjectName);
            }
        }
    }
}
