using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public GameObject mir;
    Rigidbody2D mirRigid;
    SpriteRenderer mirSpriteRenderer;
    Animator mirAnimator;
    [SerializeField] GameObject mirAttackRange;

    public float maxSpeed;   //최대 속도
    public float moveDirection;                        //Mir의 이동 방향을 정하는 변수
    public float jumpPower;
    bool isJump = false;

    int tileLayer = 1 << 6; // tile의 레이어 = 6
    string portalName;

    RaycastHit2D hit;

    void Start()
    {
        mirRigid = mir.GetComponent<Rigidbody2D>();
        mirSpriteRenderer = mir.GetComponent<SpriteRenderer>();
        mirAnimator = mir.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.P))
            mirRigid.AddForce(Vector2.up * 2.5f, ForceMode2D.Impulse);
    }

    public void Move()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        mirRigid.AddForce(Vector2.right * moveDirection, ForceMode2D.Impulse);

        if (moveDirection == 1)
        {
            mirSpriteRenderer.flipX = false;    //Mir가 오른쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if (moveDirection == -1)
        {
            mirSpriteRenderer.flipX = true;    //Mir가 왼쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if (moveDirection == 0)
        {
            mirRigid.velocity = new Vector2(0, 0);
            mirAnimator.SetBool("mirIsMove", false);
        }

        if (mirRigid.velocity.x >= maxSpeed)
            mirRigid.velocity = new Vector2(maxSpeed, mirRigid.velocity.y);
        else if (mirRigid.velocity.x <= maxSpeed * (-1))
            mirRigid.velocity = new Vector2(maxSpeed * (-1), mirRigid.velocity.y);
    }
}
