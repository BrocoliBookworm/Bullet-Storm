using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;
    public GameObject gameManagerActive;
    public GameManager gameManager;
    private bool hit = false;

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
            Debug.Log("Enemy check passed");
            hit = true;
            Debug.Log("Hit true");
            if(hit == true)
            {
                gameManager.AddScore();
                hit = false;
            }
            Debug.Log("hit false");
            Debug.Log("Called from Bullet");
        }
        Destroy(gameObject);
    }

}
