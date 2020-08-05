using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public override void Die()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        var clone = Instantiate(deathEffect, transform.position, transform.rotation);
        
        Destroy(clone, 1f);
        Destroy(gameObject);

        RandomDrop();

        GameManager.Instance().AddScore();
    }
}
