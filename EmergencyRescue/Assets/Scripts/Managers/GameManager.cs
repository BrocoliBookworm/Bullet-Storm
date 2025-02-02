﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField]
    private TextUpdate textUpdate;

    [SerializeField]
    private SurvivorUpdate survivorUpdate;

    [SerializeField]
    private RescueUpdate rescueUpdate;

    private float startTimer = 10f;

    public GameObject thePlayer;
    public GameObject theBoss;
    public bool bossSpawned = false;
    public Transform bossSpawnPoint;
    public float bossSpawnTimer;
    public bool firstSpawn = false;
    
    public GameObject deathEffect;
    public GameObject survivorSavedEffect;

    private static bool won = false;

    private static bool playing = false;

    private static bool gamePaused = false;

    [SerializeField]
    private GameObject winUI;

    [SerializeField]
    private GameObject SurvivorLocater;

    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private GameObject deathUI;

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
    public int onShipSurvivors;
    public int survivorsSaved;
    public int survivorsWin = 200;

    public Transform thePlayerShip;
    private int addScore = 10;
    
    void Awake() 
    {
        _instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        score = 0;
        survivors = 0;
        survivorsSaved = 0;
        onShipSurvivors = 0;
        SurvivorLocater.SetActive(false);

        Instantiate(thePlayer, transform.position, Quaternion.identity); 
        thePlayerShip = thePlayer.transform;
    }

    void Update()
    {   
        StartSurvivorLocater();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(survivorsSaved >= 25)
        {
            if(!bossSpawned)
            {
                if(firstSpawn)
                {
                    bossSpawnTimer -= Time.deltaTime;
                    
                    if(bossSpawnTimer <= 0)
                    {
                        bossSpawnTimer = 45f;
                        bossSpawned = true;
                        Instantiate(theBoss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                    }
                }
                else
                {
                    firstSpawn = true;
                    bossSpawned = true;
                    Instantiate(theBoss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                }
            }
            else
            {
                return;
            }
        }
    }
    
    public void AddScore()
    {
        score += addScore;
        textUpdate.UpdateScore();
    }

    public void BossKilled()
    {
        score += addScore * 3;
        textUpdate.UpdateScore();
    }

    public void AddSurvivors()
    {
        survivors++;
        survivorUpdate.UpdateSurvivors();
    }

    public void RescueSurvivors()
    {
        var clone = Instantiate(survivorSavedEffect, transform.position, transform.rotation);
        Destroy(clone, 1f);

        FindObjectOfType<AudioManager>().Play("DropOff");

        if(survivorsSaved == 0)
        {
            survivorsSaved = survivors;
        }
        else 
        {
            survivorsSaved += survivors;
        }

        if(survivorsSaved >= survivorsWin)
        {
            if(!won)
            {
                WinGame();
            }
        }

        survivors = 0;
        survivorUpdate.UpdateSurvivors();  
        rescueUpdate.UpdateRescue();
    }

    void StartSurvivorLocater()
    {
        startTimer -= Time.deltaTime;

        if(startTimer <= 0)
        {
            SurvivorLocater.SetActive(true);
        }
    }

    public void WinGame()
    {
        won = true;
        gamePaused = true;
        playing = false;
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void EndGame()
    {
        playing = false;
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        var clone = Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        Destroy(clone);
        Invoke("UICall", 1f);
        
        // Time.timeScale = 0f;
        // deathUI.SetActive(true);
    }

    public void UICall()
    {
        Time.timeScale = 0f;
        deathUI.SetActive(true);
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        playing = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        gamePaused = false;
        playing = true;
    }
}
