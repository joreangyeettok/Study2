using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        // ���� ���� �ʾ��� ������ �̵��� ������ �۵��ϵ��� ��
        if (hitState == false)
        {
            MoveCheck();
        }
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

        // ������ �̹� ���� ���̶�� �������� �������� ���ϰ� �Ѵ�.

        // ����Ű �Է��� ������
        if (jumpState == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Y������ ���� �޴´�.
                Vector2 force = new Vector2();
                force.y = jumpForce;
                GetComponent<Rigidbody2D>().AddForce(force);

                // ���� ���·� �����Ѵ�.
                jumpState = true;

                // ������ �Ǹ� ��� ����
                GetComponent<Animator>().SetBool("isJump", true);
            }
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
                hp = hp - collision.gameObject.GetComponent<MaskDude>().attackDamage;

                // �׾���.
                if (hp <= 0)
                {
                    StartCoroutine(CoDead());
                }
                else
                {
                    // �����ʰ� �¾Ҵ�.
                    StartCoroutine(CoHit(collision));
                }
            }
        }
    }

    // �ڷ�ƾ > Ư�� ���¿��� ����
    private IEnumerator CoHit(Collision2D collision)
    {
        // �÷��̾� ĳ������ �ִϸ��̼� ����
            GetComponent<Animator>().SetBool("isHit", true);

            // 1. �¾������� ��Ʈ�� �Ұ�
            hitState = true;

            UIManager.instance.ChangeHP(hp);

            // �� ĳ������ ��ġ - �� ĳ������ ��ġ
            Vector3 moveDic = transform.position - collision.transform.position;

            moveDic = Vector3.Normalize(moveDic) * knockbackFoce;

            // 2. ������ �˹�(���� �浹�� ������ �ݴ�������� �����̵���)
            GetComponent<Rigidbody2D>().AddForce(moveDic, ForceMode2D.Impulse);

            // 3. ������ HIT �����϶��� ����
            hitState = true;

            // ���̾ �����ؼ� �� ĳ���ʹ� �浹�� ���� �ʰ� ����ǰ� �Ѵ�.
            int layer = LayerMask.GetMask("Enemy");
            LayerMask layerMask = new LayerMask();
            layerMask.value = layer;
            GetComponent<BoxCollider2D>().excludeLayers = layerMask;

            // Ư�� �ð� ���� HIT ���°� �����ǰ� Ư�� �ð��� ������ HIT ���¸� ������.
            yield return new WaitForSeconds(hitSecond);
            layerMask = new LayerMask();
            GetComponent<BoxCollider2D>().excludeLayers = layerMask;

            hitState = false;
            GetComponent<Animator>().SetBool("isHit", false);
    }

    private IEnumerator CoDead()
    {
        // ���� ���� ǥ��
        UIManager.instance.ShowGameOver();

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(3);

        // ���� ���� �̸��� �ҷ�����
        string sceneName = SceneManager.GetActiveScene().name;
        
        // �̹� ���� �����
        SceneManager.LoadScene(sceneName);
    }
}
