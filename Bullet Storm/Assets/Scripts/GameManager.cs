using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; 
    public TextUpdate textUpdate;

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
    private int addScore = 10;
    Text scoreText;
    
    void Awake() 
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }
    
    public void AddScore()
    {
        score += addScore;
        textUpdate.UpdateScore();
    }
}
