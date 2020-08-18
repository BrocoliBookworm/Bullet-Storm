using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RescueUpdate : MonoBehaviour
{
    TextMeshProUGUI rescuedSurvivorsText;
    // Start is called before the first frame update
    void Start()
    {
        rescuedSurvivorsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateRescue()
    {
        rescuedSurvivorsText.text = "Survivors Rescued: " + GameManager.Instance().survivorsSaved;
    }
}
