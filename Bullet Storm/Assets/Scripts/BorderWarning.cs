using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderWarning : MonoBehaviour
{
    public static BorderWarning instance;
    
    public bool trigger = false;

    private float timer = 10f;

    public Text timerText;
    public GameObject warningUI;

    void Awake() 
    {
        instance = this;    
    }

    void Update() 
    {
        if(trigger == true)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            if(timer <= 0)
            {
                print("Dead");
                trigger = false;
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
