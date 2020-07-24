using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescueUpdate : MonoBehaviour
{
    Text rescuedSurvivorsText;
    // Start is called before the first frame update
    void Start()
    {
        rescuedSurvivorsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRescue();
    }

    public void UpdateRescue()
    {
        rescuedSurvivorsText.text = "Survivors Rescued: " + GameManager.Instance().survivorsSaved;
    }
}
