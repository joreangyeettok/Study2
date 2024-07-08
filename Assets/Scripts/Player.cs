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
        // 1. �Է��� ������
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
        // 2. �翷���� �̵�
        // ���ο� Vector3�� ����
        Vector3 horPos = new Vector3();
        // Vector3�� ���� ��ġ���� ����
        horPos = transform.position;
        // x���� ���� �Է°��� ����
        horPos.x = horPos.x + hor;

        // ���� Ʈ�������� ���� ����
        transform.position = horPos;

        // �ִϸ��̼� ����
        GetComponent<Animator>().SetBool("isMove", true);
    }
}
