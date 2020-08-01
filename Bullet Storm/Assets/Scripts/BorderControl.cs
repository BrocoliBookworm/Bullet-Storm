using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BorderControl : MonoBehaviour
{
    public static BorderControl instance;
    
    private float timer = 10f;

    private bool trigger;

    public TextMeshProUGUI timerText;
    public GameObject warningUI;

    public PlayerController playerControl;


    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
  
    }

    void Update() 
    {
        if(trigger == true)
        {
            timer -= Time.deltaTime;
            Debug.Log("timer going down");
            timerText.text = timer.ToString("F1");
            Debug.Log("Timer text called");

            if(timer <= 0)
            {
                trigger = false;
                timer = 0;
                GameManager.Instance().EndGame();
            }
        }
    }

    public void EnterArea()
    {
        warningUI.SetActive(true);
        trigger = true;
    }

    public void ExitArea()
    {
        warningUI.SetActive(false);
        trigger = false;
        timer = 10f;
    }
}
