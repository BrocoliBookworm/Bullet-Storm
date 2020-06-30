using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public int addScore;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += addScore;
        Debug.Log("Score added");
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        Debug.Log("Update Ran");
    }
}
