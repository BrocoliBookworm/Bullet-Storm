using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidnapEnemyController : EnemyController
{
    public Transform survivor;
    public Transform warpEastTarget;
    public Transform warpWestTarget;
    public GameObject survivorObject;

    public bool success = false;

    // Update is called once per frame
    void Update()
    {
        Movement();
        Target();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void SurvivorDrop()
    {
        Instantiate(survivorObject, spawnPoint.position, spawnPoint.rotation);
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
            if(success)
            {
                return;
            }
            else
            {
                success = true; 
                Destroy(other.gameObject);
            }
        }
        else if(other.gameObject.layer == 15)
        {
            if(success)
            {
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.layer == 11)
        {
            Debug.Log("hit a powerup");
        }
    }

    public override void Target()
    {
        if(survivor == null)
        {
            if(success != true)
            {
                var target = FindObjectOfType<SurvivorController>();

                if(target != null)
                {
                    survivor = target.transform;
                }
            }

            if(success == true)
            {
                if(Random.value > 0.5)
                {
                    survivor = warpEastTarget.transform;
                }
                else
                {
                    survivor = warpWestTarget.transform;
                }
            }
        }
        
        if(survivor == null)
        {
            return;
        }

        Vector3 dir = survivor.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation,desiredRot, rotationSpeed * Time.deltaTime);
    }

    public override void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(gameObject);

        if(success)
        {
            SurvivorDrop();
        }

        GameManager.Instance().AddScore();
    }
}
