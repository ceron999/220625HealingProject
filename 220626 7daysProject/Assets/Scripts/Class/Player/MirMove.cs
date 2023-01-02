using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class MirMove : MonoBehaviour
{
    [SerializeField] PortalManager portalManager;

    [SerializeField]
    GameObject mir;
    Rigidbody2D mirRigid;
    SpriteRenderer mirSpriteRenderer;
    Animator mirAnimator;
    public GameObject mirAttackRange;

    public bool isAction = false;

    [SerializeField]
    float maxSpeed;   //최대 속도
    [SerializeField]
    float maxJump;
    public float moveDirection;                        //Mir의 이동 방향을 정하는 변수
    public float jumpPower;
    bool isMove = false;
    bool isJump = false;

    int tileLayer = 1 << 6; // tile의 레이어 = 6

    RaycastHit2D hit;

    void Start()
    {
        mirRigid = mir.GetComponent<Rigidbody2D>();
        mirSpriteRenderer = mir.GetComponent<SpriteRenderer>();
        mirAnimator = mir.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAction)
        {
            Move();   //이동 버튼이 눌려있다면 Mir가 움직인다.
            Jump();
            Attack();
        }
        else
        {
            moveDirection = 0;
        }
    }

    public IEnumerator MoveToDest(Transform dest, float endTime)
    {
        Vector2 nowPos = mirRigid.position;
        Vector2 destPos = dest.position;
        destPos.x += 0.2f;

        if (destPos.x > nowPos.x)
            moveDirection = 1;
        else if (destPos.x < nowPos.x)
            moveDirection = -1;
        else
            moveDirection = 0;

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
            mirRigid.velocity = new Vector2(0, mirRigid.velocity.y);
            mirAnimator.SetBool("mirIsMove", false);
        }

        float startTime = 0;
        while(startTime < endTime)
        {
            mirAnimator.SetBool("mirIsMove", true);
            startTime += (Time.deltaTime);

            mirRigid.transform.position = Vector3.Lerp(nowPos, destPos, startTime / endTime);

            yield return null;
        }
        mirAnimator.SetBool("mirIsMove", false);
    }

    public void Move()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        isMove = true;

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
            mirRigid.velocity = new Vector2(0, mirRigid.velocity.y);
            mirAnimator.SetBool("mirIsMove", false);
            isMove = false;
        }

        if (mirRigid.velocity.x >= maxSpeed)
            mirRigid.velocity = new Vector2(maxSpeed, mirRigid.velocity.y);
        else if (mirRigid.velocity.x <= maxSpeed * (-1))
            mirRigid.velocity = new Vector2(maxSpeed * (-1), mirRigid.velocity.y);
    }

    void Jump()
    {
        //Debug.Log(mirRigid.velocity);
        Debug.DrawRay(mirRigid.position, Vector2.down * 0.3f, Color.red);
        hit = Physics2D.Raycast(mirRigid.position, Vector2.down, 0.3f, tileLayer);
        
        if (hit.collider != null)
        {
            if (hit.transform.name == "Tilemap")
            {
                isJump = false;
            }
        }
        else
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.C) && !isJump)
        {
            mirRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            if (mirRigid.velocity.y >= maxJump)
                mirRigid.velocity = new Vector2(mirRigid.velocity.x, maxJump);
        }

        if (isJump && !isMove)
        {
            hit = Physics2D.Raycast(mirRigid.position, Vector2.down, 0.7f, tileLayer);
            if (hit.collider != null)
            {
                return;
            }
            
            if (mirRigid.velocity.y > 0.5)
            {
                mirRigid.velocity = new Vector2(mirRigid.velocity.x, 0);
            }
        }
    }
    
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mirAttackRange.SetActive(true);
            mirAnimator.Play("MirAttack");
            
        }

        //공격 모션이 끝나면 공격 콜라이더를 비활성화시킴
        if (mirAnimator.GetCurrentAnimatorStateInfo(0).IsName("MirAttack")
            && mirAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            mirAttackRange.SetActive(false);
        }
    }
}
