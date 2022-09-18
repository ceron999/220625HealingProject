using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    public GameObject mir;
    Rigidbody2D mirRigid;
    SpriteRenderer mirSpriteRenderer;
    Animator mirAnimator;
    public float maxSpeed = 2.0f;   //최대 속도
    public float moveSpeed;                        //Mir의 이동 방향을 정하는 변수
    public float jumpSpeed = 2.0f;
    bool isJump = false;

    RaycastHit2D hit;

    void Start()
    {
        mirRigid = mir.GetComponent<Rigidbody2D>();
        mirSpriteRenderer = mir.GetComponent<SpriteRenderer>();
        mirAnimator = mir.GetComponent<Animator>();
    }

    void Update()
    {
        Move();   //이동 버튼이 눌려있다면 Mir가 움직인다.
        Jump();
    }

    private void FixedUpdate()
    {
    }

    public void Move()
    {
        moveSpeed = Input.GetAxisRaw("Horizontal");

        mirRigid.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);

        if(moveSpeed == 1)
        {
            mirSpriteRenderer.flipX = false;    //Mir가 오른쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if(moveSpeed == -1)
        {
            mirSpriteRenderer.flipX = true;    //Mir가 왼쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if(moveSpeed == 0)
        {
            mirRigid.velocity = new Vector2(0,0);
            mirAnimator.SetBool("mirIsMove", false);
        }

        if (mirRigid.velocity.x >= maxSpeed)
            mirRigid.velocity = new Vector2(maxSpeed, mirRigid.velocity.y);
        else if (mirRigid.velocity.x <= maxSpeed * (-1))
            mirRigid.velocity = new Vector2(maxSpeed * (-1), mirRigid.velocity.y);
    }

    void Jump()
    {
        Debug.DrawRay(mirRigid.position, Vector2.down * 0.4f, Color.red);
        hit = Physics2D.Raycast(mirRigid.position, Vector2.down, 0.5f);

        if (hit.collider != null)
            Debug.Log(hit.transform.name);

        if (Input.GetKeyDown(KeyCode.C) && !isJump)
        {
            mirRigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            //isJump = true;
        }
    }
}
