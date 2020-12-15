using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidnapEnemyController : EnemyController
{
    public Transform targetLocation;
    public Transform warpEastTarget;
    public Transform warpWestTarget;
    public GameObject survivorObject;
    public GameObject onShipSurvivorObject;
    public bool shipSurvivor = false;
    public bool regularSurvivor = false;

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
        if(shipSurvivor)
        {
            Instantiate(onShipSurvivorObject, spawnPoint.position, spawnPoint.rotation);
        }
        
        if(regularSurvivor)
        {
            Instantiate(survivorObject, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public override void Target()
    {
        if(targetLocation == null)
        {
            if(success != true)
            {
                var target = FindObjectOfType<SurvivorController>();
            
                if(target != null)
                {
                    targetLocation = target.transform;
                }
            }

            if(success == true)
            {
                if(Random.value > 0.5)
                {
                    targetLocation = warpEastTarget.transform;
                }
                else
                {
                    targetLocation = warpWestTarget.transform;
                }
            }
        }
        
        if(targetLocation == null)
        {
            return;
        }

        Vector3 dir = targetLocation.position - transform.position;
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

    protected override void CollideWithSurvivor(SurvivorController survivor)
    {
        if(success)
        {
            return;
        }
        else
        {
            if(survivor.GetComponent<OnShipSurvivorsController>())
            {
                Destroy(survivor.gameObject);
                shipSurvivor = true;
            }
            else
            {
                Destroy(survivor.gameObject);
                regularSurvivor = true;
            }

            success = true;
            targetLocation = null;
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
