using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour
{
    public float lifespan;

    void Update() 
    {
        Death();    
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerController>())
        {
            if(GameManager.Instance().survivors <= 29)
            {
                Destroy(gameObject);
            }
            else if(GameManager.Instance().survivors >= 30)
            {
                return;
            }
        }
    }

    void Death()
    {
        if(lifespan > 0)
        {
            lifespan -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
