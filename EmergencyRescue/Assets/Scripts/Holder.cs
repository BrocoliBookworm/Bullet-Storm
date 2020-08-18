using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public GameObject[] weapons;
    private bool firstCheck = false;
    private bool firstSecondCheck = false;
    private bool first = false;
    private bool second = false;

    void Update()
    {
        Check();

        if(first && !firstCheck)
        {
            FindObjectOfType<AudioManager>().Play("WeaponShotChange");
            firstCheck = true;
        }

        if(second && !firstSecondCheck)
        {
            FindObjectOfType<AudioManager>().Play("WeaponShotChange");
            firstSecondCheck = true;
        }
    }

    void Check()
    {
        if(GameManager.Instance().onShipSurvivors >= 10)
        {
            first = true;
            weapons[1].SetActive(true);
        }

        if(GameManager.Instance().onShipSurvivors >= 20)
        {
            second = true;            
            weapons[2].SetActive(true);
        }
    }
}
