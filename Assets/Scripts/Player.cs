using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCheck();
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
}
