using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(gameObject.tag == other.tag)
        {
            return;
        }

        if(other.GetComponent<KidnapEnemyController>() || other.GetComponent<BasicEnemyController>() || other.GetComponent<TurretEnemyController>() || other.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }

        if(other.GetComponent<PowerUp>())
        {
            return;
        }
    }
}
