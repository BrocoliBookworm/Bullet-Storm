using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAreaTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            BorderWarning.instance.EnterArea();
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            BorderWarning.instance.ExitArea();
        }
    }
}
