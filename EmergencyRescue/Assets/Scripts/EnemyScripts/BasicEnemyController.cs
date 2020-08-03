using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    public override void Target()
    {
        
        if(player == null)
        {
            //Find the players ship
            target = GameObject.FindWithTag("Player").transform;
            
            if(target != null)
            {
                player = target.transform;
            }
        }

        //We've either found the player or the player doesn't exist rn
        
        if(player == null)
        {
            return; //Try next frame
        }

        //Here we know there is a player. Go get it
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotationSpeed * Time.deltaTime);
    }

    public override void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(gameObject);

        RandomDrop();

        GameManager.Instance().AddScore();
    }
}
