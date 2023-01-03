using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    GameObject oldMan;
    Rigidbody2D oldManRigid;
    SpriteRenderer oldManSpriteRenderer;
    Animator oldManAnimator;

    public float moveDirection;

    void Start()
    {
        oldManRigid = this.GetComponent<Rigidbody2D>();
        oldManSpriteRenderer = this.GetComponent<SpriteRenderer>();
        oldManAnimator = this.GetComponent<Animator>();
    }
    public IEnumerator MoveToDest(Transform dest, float endTime)
    {
        Vector2 nowPos = oldManRigid.position;
        Vector2 destPos = dest.position;
        destPos.y = nowPos.y;

        if (destPos.x > nowPos.x)
            moveDirection = 1;
        else if (destPos.x < nowPos.x)
            moveDirection = -1;
        else
            moveDirection = 0;

        if (moveDirection == 1)
        {
            oldManSpriteRenderer.flipX = false;    //Mir가 오른쪽을 쳐다보게 합니다.
            oldManAnimator.SetBool("oldManIsWalk", true); //Mir가 Move animation을 실행합니다.
        }
        else if (moveDirection == -1)
        {
            oldManSpriteRenderer.flipX = true;    //Mir가 왼쪽을 쳐다보게 합니다.
            oldManAnimator.SetBool("oldManIsWalk", true); //Mir가 Move animation을 실행합니다.
        }
        else if (moveDirection == 0)
        {
            oldManRigid.velocity = new Vector2(0, oldManRigid.velocity.y);
            oldManAnimator.SetBool("oldManIsWalk", false);
        }

        float startTime = 0;
        while (startTime < endTime)
        {
            oldManAnimator.SetBool("oldManIsWalk", true);
            startTime += (Time.deltaTime);

            oldManRigid.transform.position = Vector3.Lerp(nowPos, destPos, startTime / endTime);

            yield return null;
        }
        oldManAnimator.SetBool("oldManIsWalk", false);
    }
}
