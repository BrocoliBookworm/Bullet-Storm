﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public TextUpdate textUpdate;
    public SurvivorUpdate survivorUpdate;
    public RescueUpdate rescueUpdate;

    public GameObject thePlayer;

    public static GameManager Instance()
    {
        if(_instance == null)
        {
            GameObject go = new GameObject("GameManager"); //assign instance to this instance of the class
            go.AddComponent<GameManager>();
        }

        return _instance;
    }

    public int score;
    public int survivors;
    public int survivorsSaved;
    private int addScore = 10;
    
    void Awake() 
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        survivors = 0;
        survivorsSaved = 0;
        Instantiate(thePlayer, transform.position, Quaternion.identity); 
    }
    
    public void AddScore()
    {
        score += addScore;
        textUpdate.UpdateScore();
    }

    public void AddSurvivors()
    {
        survivors++;
        survivorUpdate.UpdateSurvivors();
    }

    public void RescueSurvivors()
    {
        if(survivorsSaved == 0)
        {
            survivorsSaved = survivors;
        }
        else
        {
            survivorsSaved += survivors;
        }
        survivors = 0;
        survivorUpdate.UpdateSurvivors();  
        rescueUpdate.UpdateRescue();
    }
}
