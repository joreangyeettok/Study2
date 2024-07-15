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
        // �ݺ���
        // �ʱⰪ�� ���Ѵ�. �ݺ����� ������ ����, �ݺ��ѹ� �Ҷ����� �����Ǵ� ��

        if (moveStop == false)
        {
            // �̵� �Ѵ�.
            float moveTime = moveSecond * Time.deltaTime;
            Vector3 movePos = Vector3.MoveTowards(transform.position, moveObjs[currentTargetPos].position, moveTime);
            transform.position = movePos;

            // Ư�� ������ �����ߴٸ� ������������ ����.
            float dis = Vector3.Distance(transform.position, moveObjs[currentTargetPos].position);
            if (dis <= moveCheckValue)
            {
                // ���� ��������
                currentTargetPos = currentTargetPos + 1;

                // �ش��ϴ� ��ġ���� ������ ���� Ÿ�� ��ġ�� �����ϴٸ�
                if (moveObjs.Count <= currentTargetPos)
                {
                    // Ÿ���� 0���� �ϵ��� ��
                    currentTargetPos = 0;
                }
            }
        }
    }
}
