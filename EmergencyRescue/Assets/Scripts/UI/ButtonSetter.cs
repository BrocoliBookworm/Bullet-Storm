using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetter : MonoBehaviour
{
    public float timer;

    public GameObject playButton;

    public GameObject backButton;

    void Start()
    {
        timer = 20f;
        playButton = GameObject.Find("PlayButton");
        backButton = GameObject.Find("BackButton (1)");
        playButton.SetActive(false);
        backButton.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            playButton.SetActive(true);
            backButton.SetActive(true);
        }
    }


}
