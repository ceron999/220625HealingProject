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

    public float maxSpeed;   //�ִ� �ӵ�
    public float moveDirection;                        //Mir�� �̵� ������ ���ϴ� ����
    public float jumpPower;
    bool isJump = false;

    int tileLayer = 1 << 6; // tile�� ���̾� = 6
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
            mirSpriteRenderer.flipX = false;    //Mir�� �������� �Ĵٺ��� �մϴ�.
            mirAnimator.SetBool("mirIsMove", true); //Mir�� Move animation�� �����մϴ�.
        }
        else if (moveDirection == -1)
        {
            mirSpriteRenderer.flipX = true;    //Mir�� ������ �Ĵٺ��� �մϴ�.
            mirAnimator.SetBool("mirIsMove", true); //Mir�� Move animation�� �����մϴ�.
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
