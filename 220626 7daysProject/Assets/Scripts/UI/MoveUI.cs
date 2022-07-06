using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour
{
    //UI 오브젝트
    public Button leftBtn;
    public Button rightBtn;

    //Mir 오브젝트
    public GameObject mir;
    Rigidbody2D mirRigid;
    SpriteRenderer mirSpriteRenderer;
    Animator mirAnimator;

    //이동 관련 변수
    bool isLeftBtnClicked = false;      //이동 전용 버튼이 클릭되었다면 '참'이 되는 변수
    bool isRightBtnClicked = false;      //이동 전용 버튼이 클릭되었다면 '참'이 되는 변수
    public float maxSpeed = 2.0f;   //최대 속도
    public float h;                        //Mir의 이동 방향을 정하는 변수

    void Start()
    {
        mirRigid = mir.GetComponent<Rigidbody2D>();
        mirSpriteRenderer = mir.GetComponent<SpriteRenderer>();
        mirAnimator = mir.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isLeftBtnClicked || isRightBtnClicked) Move();   //이동 버튼이 눌려있다면 Mir가 움직인다.
    }

    public void LeftBtnPointerDown()
    {
        mirSpriteRenderer.flipX = true; //Mir가 왼쪽을 쳐다보게 합니다.

        mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.

        isLeftBtnClicked = true;
        h = -1;
    }

    public void LeftBtnPointerUp()
    {
        isLeftBtnClicked = false;
        h = 0;

        mirAnimator.SetBool("mirIsMove", false); //Mir가 Move animation을 중단합니다.

        mirRigid.velocity = new Vector2(0, mirRigid.velocity.y);
    }

    public void RightBtnPointerDown()
    {
        mirSpriteRenderer.flipX = false;    //Mir가 오른쪽을 쳐다보게 합니다.

        mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.

        isRightBtnClicked = true;
        h = 1;
    }

    public void RightBtnPointerUp()
    {
        isRightBtnClicked = false;
        h = 0;

        mirAnimator.SetBool("mirIsMove", false); //Mir가 Move animation을 중단합니다.

        mirRigid.velocity = new Vector2(0, mirRigid.velocity.y);
    }

    public void Move()
    {
        mirRigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (mirRigid.velocity.x >= maxSpeed)
            mirRigid.velocity = new Vector2(maxSpeed, mirRigid.velocity.y);
        else if (mirRigid.velocity.x <= maxSpeed * (-1))
            mirRigid.velocity = new Vector2(maxSpeed * (-1), mirRigid.velocity.y);
    }
}
