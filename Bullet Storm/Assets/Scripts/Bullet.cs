using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
            GameManager.Instance().AddScore();
        }
        else if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == 11)
        {
            Debug.Log("hit a powerup");
        }
    }

}
