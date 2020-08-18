using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurvivorUpdate : MonoBehaviour
{
    TextMeshProUGUI survivorOnBoardText;
    // Start is called before the first frame update
    void Start()
    {
        survivorOnBoardText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSurvivors()
    {
        survivorOnBoardText.text = "Survivors On Board: " + GameManager.Instance().survivors;
    }
}
