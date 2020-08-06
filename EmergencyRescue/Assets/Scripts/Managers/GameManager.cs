using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private TextUpdate textUpdate;
    private SurvivorUpdate survivorUpdate;
    private RescueUpdate rescueUpdate;

    public GameObject thePlayer;

    public static bool won = false;

    public static bool playing = false;

    public static bool gamePaused = false;

    public GameObject winUI;

    public GameObject pauseUI;

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
        playing = true;
        score = 0;
        survivors = 0;
        survivorsSaved = 0;
        Instantiate(thePlayer, transform.position, Quaternion.identity); 
    }

    void Update()
    {
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

        if(survivorsSaved >= 199)
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
        MainMenu.Instance().GameOver();
        // Invoke("NextScreen", 4f);
        playing = false;
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
        Debug.Log("resume");
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        playing = true;
    }

    // public void NextScreen()
    // {
    //     MainMenu.Instance().GameOver();
    // }
}
