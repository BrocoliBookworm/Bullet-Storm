using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : HealthManager
{
    public float rotationSpeed;
    public Transform player;

    public GameObject deathEffect;

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
        if(other.GetComponent<EnemyController>())
        {
            return;
        }

        if(other.GetComponent<Bullet>())
        {
            if(other.GetComponent<ExplodingBullet>())
            {
                CollideWithBullet(3);
                return;
            }
            else
            {
                CollideWithBullet(1);
                return;
            }
        }

        // if(other.GetComponent<ExplodingBullet>())
        // {
        //     CollideWithBullet(3);
        //     Debug.Log("damage3");
        //     return;
        // }

        if(other.GetComponent<PlayerController>())
        {
            CollideWithPlayer();
            return;
        }

        if(other.GetComponent<SurvivorController>())
        {
            if(gameObject.GetComponent<KidnapEnemyController>().success != true)
            {
                if(other.GetComponent<OnShipSurvivorsController>())
                {
                    Destroy(other.gameObject);
                    gameObject.GetComponent<KidnapEnemyController>().shipSurvivor = true;
                }
                else
                {
                    Destroy(other.gameObject);
                    gameObject.GetComponent<KidnapEnemyController>().regularSurvivor = true;
                }

                CollideWithSurvivor();
            }
            return;
        }

        if(other.gameObject.layer == 15)
        {
            CollideWithWarp();
            return;
        }

        if(other.GetComponent<PowerUp>())
        {
            return;
        }
    }

    public void RandomDrop()
    {
        if(Random.value > 0.8)
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

    protected virtual void CollideWithBullet(int damageTaken)
    {
        currentHealth = currentHealth - damageTaken;
    }

    protected virtual void CollideWithPlayer()
    {
        currentHealth--;
    }

    protected virtual void CollideWithSurvivor()
    {

    }

    protected virtual void CollideWithWarp()
    {

    }
}
