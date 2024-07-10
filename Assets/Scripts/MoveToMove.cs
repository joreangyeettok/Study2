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
        // �ʱⰪ�� ���Ѵ�. �ݺ����� ������ ����, �ݺ��ѹ� �Ҷ����� �����Ǵ� ��
        for (int i = 0; i < moveObjs.Count; i++)
        {
            
        }

        // ���� �ߴٸ�
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

        // 1. ��Ʈ�Ѹ����
        // 2. ��Ʈ �����϶� ��ĳ���� �浹���� �ʰ� �ϱ�
    }
}
