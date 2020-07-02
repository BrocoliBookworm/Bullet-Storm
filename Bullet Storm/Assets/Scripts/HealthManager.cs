using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    float invulnerableTimer = 0;
    int correctLayer;
    public bool collision = false;
    public PlayerController playerControl;
    public int collisionDamage = 1;
    SpriteRenderer spriteRend;

    void Start() 
    {
        correctLayer = gameObject.layer;
        if(gameObject.layer == 8)
        {
            health = playerControl.maxHealth;
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
    void OnTriggerEnter2D() 
    {
        health --;
        collision = true;

        invulnerableTimer = 0.5f;

        gameObject.layer = 10;
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

        if(health <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        Destroy(gameObject);    
    }
}
