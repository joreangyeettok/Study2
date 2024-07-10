using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMove : MonoBehaviour
{
    public List<Transform> moveObjs = new List<Transform>();

    public float moveSecond = 3f;

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
        // 초기값을 정한다. 반복에서 나가는 조건, 반복한번 할때마다 수정되는 값
        for (int i = 0; i < moveObjs.Count; i++)
        {
            
        }

        // 도착 했다면
        if (transform.position == moveObjs[0].position)
        {
            float moveTime = moveSecond * Time.deltaTime;
            Vector3 movePos = Vector3.MoveTowards(transform.position, moveObjs[1].position, moveTime);
            transform.position = movePos;
        }
        else
        {
            float moveTime = moveSecond * Time.deltaTime;
            Vector3 movePos = Vector3.MoveTowards(transform.position, moveObjs[0].position, moveTime);
            transform.position = movePos;
        }

        // 1. 패트롤만들기
        // 2. 히트 상태일때 적캐릭터 충돌되지 않게 하기
    }
}
