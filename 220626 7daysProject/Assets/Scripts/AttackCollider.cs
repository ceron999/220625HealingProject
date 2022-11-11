using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.transform.tag == "Goal")
            {
                Debug.Log(collision.transform.name);
                SetGoalClear(collision.transform.name);
            }
        }
    }

    void SetGoalClear(string collisionName)
    {
        switch (collisionName)
        {
            case "TutorialGoal":
                GameManager.gameManager.isTutorialGoalClear = true;
                break;
        }
            
    }
}
