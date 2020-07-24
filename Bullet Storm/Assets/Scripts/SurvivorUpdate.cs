using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivorUpdate : MonoBehaviour
{
    Text survivorOnBoardText;
    // Start is called before the first frame update
    void Start()
    {
        survivorOnBoardText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSurvivors();
    }

    public void UpdateSurvivors()
    {
        survivorOnBoardText.text = "Survivors On Board: " + GameManager.Instance().survivors;
    }
}
