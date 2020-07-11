using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    float invulnerableTimer = 0;
    int correctLayer;
    public bool collision = false;
    public PlayerController playerControl;
    public int collisionDamage = 1;
    SpriteRenderer spriteRend;
    public bool shotHealthPowerUp = false;

    public EnemyController enemyController;

    void Start() 
    {
        correctLayer = gameObject.layer;
        if(gameObject.layer == 8)
        {
            maxHealth = playerControl.maxHealth;
            currentHealth = playerControl.maxHealth;
        }

        //Only gets the renderer on the parent object
        //Doesn't work for children such as the enemies
        spriteRend = GetComponent<SpriteRenderer>();
        
        if(spriteRend == null)
        {
            spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

            if(spriteRend == null)
            {
                Debug.Log("Object " + gameObject.name + " has no sprite renderer");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("HealthPowerUp"))
        {
            currentHealth--;
            playerControl.currentHealth = currentHealth;
            collision = true;

            invulnerableTimer = 0.5f;

            gameObject.layer = 10;   
        }
        else if(other.CompareTag("HealthPowerUp"))
        {
            if(gameObject.layer == 8)
            {
                if(currentHealth < maxHealth)
                {
                    shotHealthPowerUp = true;
                    currentHealth++;
                    playerControl.currentHealth = currentHealth;
                    shotHealthPowerUp = false;
                }
            }
        }
    }

    void Update() 
    {   
        if(invulnerableTimer > 0)
        {
            invulnerableTimer -=Time.deltaTime;
            //StartCoroutine(cameraShake.Shake(.15f, .4f));
        
            if(invulnerableTimer <= 0)
            {
                gameObject.layer = correctLayer;

                if(spriteRend != null) 
                {
					spriteRend.enabled = true;
				}
			}
			else 
            {
				if(spriteRend != null) 
                {
					spriteRend.enabled = !spriteRend.enabled;
                }
			}
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        Destroy(gameObject);  
        if(gameObject.layer == 9)
        {
            enemyController.RandomDrop();
        }  
    }
}
