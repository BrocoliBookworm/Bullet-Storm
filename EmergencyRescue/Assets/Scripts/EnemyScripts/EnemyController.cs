using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : HealthManager
{
    public float rotationSpeed;
    public Transform player;

    public Transform target;
    public float speed;

    public GameObject powerUp;
    public Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(gameObject.tag == other.tag)
        {
            return;
        }

        if(other.gameObject.layer == 8)
        {
            if(!other.gameObject.GetComponent<ExplodingBullet>())
            {
                TakeDamage();
            }
            else
            {
                currentHealth = currentHealth - 3;
            }
        }
        else if(other.gameObject.layer == 14)
        {
            if(!gameObject.GetComponent<KidnapEnemyController>())
            {
                Debug.Log("non-kidnap made contact");
            }
        }
        else if(other.gameObject.layer == 11)
        {
            Debug.Log("hit a powerup");
        }
    }

    public void RandomDrop()
    {
        if(Random.value > 0.7)
        {
            Instantiate(powerUp, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void Movement()
    {
        Vector3 pos = transform.position;
        
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
    }

    public virtual void Target()
    {

    }

    public override void TakeDamage()
    {
        currentHealth--;
    }
}
