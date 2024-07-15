using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public  static UIManager instance = null;
    private UIManager _instance { get { return instance; } }

    public TextMeshProUGUI hpText = null;

    public GameObject gameOverObj = null;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeHP(int _hp)
    { 
        hpText.text = "HP " + _hp;
    }

    public void ShowGameOver()
    {
        // È°¼ºÈ­
        gameObject.SetActive(true);
        gameOverObj.GetComponentInChildren<TextMeshProUGUI>().text = "GAME OVER";
    }

    public void ShowGameClear()
    {
        gameObject.SetActive(true);
        gameOverObj.GetComponentInChildren<TextMeshProUGUI>().text = "GAME CLEAR";
    }
}
