using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class TempMove : MonoBehaviour
{
    [SerializeField] PortalManager portalManager;

    public GameObject mir;
    Rigidbody2D mirRigid;
    SpriteRenderer mirSpriteRenderer;
    Animator mirAnimator;

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
        Move();   //이동 버튼이 눌려있다면 Mir가 움직인다.
        Jump();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            portalName = collision.transform.name;
            
            if (collision.transform.tag == "Portal")
                TakePortal(portalName);
        }
    }

    public void Move()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        mirRigid.AddForce(Vector2.right * moveDirection, ForceMode2D.Impulse);

        if(moveDirection == 1)
        {
            mirSpriteRenderer.flipX = false;    //Mir가 오른쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if(moveDirection == -1)
        {
            mirSpriteRenderer.flipX = true;    //Mir가 왼쪽을 쳐다보게 합니다.
            mirAnimator.SetBool("mirIsMove", true); //Mir가 Move animation을 실행합니다.
        }
        else if(moveDirection == 0)
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
        }

        if (isJump && mirRigid.velocity.x == 0)
        {
            mirRigid.AddForce(Vector2.down * 2, ForceMode2D.Impulse);
        }
    }

    void TakePortal(string getPortalName)
    {
        if(portalManager.CanTakePortal(getPortalName))
        {
            //씬 이름 얻는 중
            StringBuilder sceneName = new StringBuilder();
            sceneName.Append(getPortalName);
            sceneName.Remove(getPortalName.Length - 6, 6);

            //해당 씬으로 이동
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}
