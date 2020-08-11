using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu _instance;

    public static MainMenu Instance()
    {
        if(_instance == null)
        {
            GameObject go = new GameObject("GameManager"); //assign instance to this instance of the class
            go.AddComponent<GameManager>();
        }

        return _instance;
    }

    void Awake() 
    {
        _instance = this;
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene(1);
    }

    public void Title()
    {
        FindObjectOfType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene(0);
    }
}
