using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMove : MonoBehaviour
{
    public List<Transform> moveObjs = new List<Transform>();

    public float moveSecond = 3f;

    public bool moveStop = false;

    public float moveCheckValue = 0.3f;

    private int currentTargetPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // 반복문
        // 초기값을 정한다. 반복에서 나가는 조건, 반복한번 할때마다 수정되는 값

        if (moveStop == false)
        {
            // 이동 한다.
            float moveTime = moveSecond * Time.deltaTime;
            Vector3 movePos = Vector3.MoveTowards(transform.position, moveObjs[currentTargetPos].position, moveTime);
            transform.position = movePos;

            // 특정 지점에 도착했다면 다음지점으로 간다.
            float dis = Vector3.Distance(transform.position, moveObjs[currentTargetPos].position);
            if (dis <= moveCheckValue)
            {
                // 다음 지점으로
                currentTargetPos = currentTargetPos + 1;

                // 해당하는 위치들의 개수와 현재 타깃 위치가 동일하다면
                if (moveObjs.Count <= currentTargetPos)
                {
                    // 타깃을 0부터 하도록 함
                    currentTargetPos = 0;
                }
            }
        }
    }
}
