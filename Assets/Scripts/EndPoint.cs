using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public string nextStageName = "";

    public float waitSecond = 3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(CoStageClear());
        }
    }

    private IEnumerator CoStageClear()
    {
        // ���� Ŭ���� UI ǥ��
        UIManager.instance.ShowGameClear();

        // ���� �ð����� ����Ѵ�.
        yield return new WaitForSeconds(waitSecond);

        // ���� ���������� �Ѿ��.
        SceneManager.LoadScene(nextStageName);
    }
}
