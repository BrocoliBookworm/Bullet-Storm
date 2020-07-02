using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 
    public int score;
    public int addScore;
    Text scoreText;

    /*void Awake() 
    {
        if(instance == null)
        {
            instance = this; //assign instance to this instance of the class
        }
        else if(instance != this) 
        {
            Destroy(gameObject); //Removing any chance of duplicates, because we already have the manager assigned somewhere else
        }
        score = 0;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += addScore;
        UpdateScore();
        Debug.Log("Score added");
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        Debug.Log("Update Ran");
    }
}
