﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetter : MonoBehaviour
{
    private static ButtonSetter _instance;

    public float timer;

    public GameObject playButton;

    public GameObject backButton;

    public bool wentBack = false;

    public bool goForwards = false;

    public static ButtonSetter Instance()
    {
        if(_instance == null)
        {
            GameObject go = new GameObject("Story"); //assign instance to this instance of the class
            go.AddComponent<ButtonSetter>();
        }

        return _instance;
    }

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

    public void Backer()
    {
        if(!wentBack)
        {
            wentBack = true;
            goForwards = false;
        }
    }

    public void Forwards()
    {
        if(!goForwards)
        {
            goForwards = true;
            wentBack = false;
        }

        if(goForwards)
        {
            timer = 20f;
            playButton.SetActive(false);
            backButton.SetActive(false);
        }
    }
}
