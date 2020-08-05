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
        var clone = Instantiate(deathEffect, transform.position, transform.rotation);
        
        Destroy(clone, 1f);
        Destroy(gameObject);

        if(success)
        {
            SurvivorDrop();
        }

        GameManager.Instance().AddScore();
    }

    protected override void CollideWithSurvivor()
    {
        if(success)
        {
            return;
        }
        else
        {
            success = true;
        }
    }

    protected override void CollideWithWarp()
    {
        if(success)
        {
            Destroy(gameObject);
        }
    }
}
