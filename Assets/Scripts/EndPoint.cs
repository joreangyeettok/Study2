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
        // 게임 클리어 UI 표시
        UIManager.instance.ShowGameClear();

        // 일정 시간동안 대기한다.
        yield return new WaitForSeconds(waitSecond);

        // 다음 스테이지로 넘어간다.
        SceneManager.LoadScene(nextStageName);
    }
}
