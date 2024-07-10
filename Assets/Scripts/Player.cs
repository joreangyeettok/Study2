using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float moveSpeed = 0.1f;
    public float jumpForce = 1f;
    public float knockbackFoce = 10f;
    public float hitSecond = 3f;
    private bool jumpState = false;
    private bool hitState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 내가 맞지 않았을 때에만 이동과 점프가 작동하도록 함
        if (hitState == false)
        {
            MoveCheck();
        }
    }

    private void MoveCheck()
    {
        float hor = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        // 1. 입력을 받으면
        if (hor < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Move(hor);
        }
        else if (hor > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Move(hor);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMove", false);
        }

        // 점프가 이미 실행 중이라면 연속으로 점프하지 못하게 한다.

        // 점프키 입력을 받으면
        if (jumpState == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Y축으로 힘을 받는다.
                Vector2 force = new Vector2();
                force.y = jumpForce;
                GetComponent<Rigidbody2D>().AddForce(force);

                // 점프 상태로 변경한다.
                jumpState = true;

                // 점프가 되면 모션 변경
                GetComponent<Animator>().SetBool("isJump", true);
            }
        }
    }
    private void Move(float hor)
    {
        // 2. 양옆으로 이동
        // 새로운 Vector3을 생성
        Vector3 horPos = new Vector3();
        // Vector3에 기존 위치값을 복사
        horPos = transform.position;
        // x축의 값에 입력값을 더함
        horPos.x = horPos.x + hor;

        // 실제 트랜스폼의 값을 변경
        transform.position = horPos;

        // 애니메이션 변경
        GetComponent<Animator>().SetBool("isMove", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpState = false;
            GetComponent<Animator>().SetBool("isJump", false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (hitState == false)
            {
                StartCoroutine(CoHit(collision));
            }
        }
    }

    // 코루틴 > 특정 상태에만 가동
    private IEnumerator CoHit(Collision2D collision)
    {
        // 적에게 맞았다.
        // 적에게 공격력만큼 HP가 감소했다
        hp = hp - collision.gameObject.GetComponent<MaskDude>().attackDamage;

        // 플레이어 캐릭터의 애니메이션 변경
        GetComponent<Animator>().SetBool("isHit", true);

        // 1. 맞았을때는 컨트롤 불가
        hitState = true;

        // 적 캐릭터의 위치 - 내 캐릭터의 위치
        Vector3 moveDic = transform.position - collision.transform.position;

        moveDic = Vector3.Normalize(moveDic) * knockbackFoce;

        // 2. 맞으면 넉백(내가 충돌한 방향의 반대방향으로 움직이도록)
        GetComponent<Rigidbody2D>().AddForce(moveDic, ForceMode2D.Impulse);

        // 3. 맞으면 HIT 상태일때는 무적
        hitState = true;

        // 특정 시간 동안 HIT 상태가 유지되고 특정 시간이 끝나면 HIT 상태를 끝낸다.
        yield return new WaitForSeconds(hitSecond);

        hitState = false;
        GetComponent<Animator>().SetBool("isHit", false);
    }
}
