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
    public KidnapEnemyController kidnapEnemyController;

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
        if(gameObject.tag == other.tag)
        {
            return;
        }

        if(other.CompareTag("HealthPowerUp"))
        {
            if(gameObject.layer == 8)
            {
                FindObjectOfType<AudioManager>().Play("PowerUpSound");
                if(currentHealth < maxHealth)
                {
                    shotHealthPowerUp = true;
                    currentHealth++;
                    playerControl.currentHealth = currentHealth;
                }
            }
        }
        else if(other.CompareTag("SpaceStation"))
        {
            GameManager.Instance().RescueSurvivors();
        }
        else if(other.CompareTag("Survivor") && gameObject.tag == "Player")
        {
            GameManager.Instance().AddSurvivors();
        }
        else if(other.CompareTag("Survivor") && gameObject.tag == "KidnapEnemy")
        {
            kidnapEnemyController.success = true;
            Destroy(other.gameObject);
        }
        else
        {
            if(gameObject.layer == 8)
            {
                Debug.Log("gameobject player");
                currentHealth--;
                playerControl.currentHealth = currentHealth;

                collision = true;

                invulnerableTimer = 0.5f;

                gameObject.layer = 10;  
            }

            if(gameObject.layer == 9 && other.CompareTag("ExplosiveShot"))
            {
                currentHealth = currentHealth - 3;
            }
            else
            {
                currentHealth--;
            }
        }
    }

    void Update() 
    {   
        if(invulnerableTimer > 0)
        {
            invulnerableTimer -=Time.deltaTime;
        
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
            if(gameObject.layer == 8)
            {
                Debug.Log("player layer");
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                GameManager.Instance().EndGame();
            }
            Die();
        }
    }

    void Die() 
    {
        if(gameObject.layer == 9)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            Destroy(gameObject);
            if(gameObject.tag == "KidnapEnemy")
            {
                if(kidnapEnemyController.success)
                {
                    kidnapEnemyController.SurvivorDrop();
                }
            } 
            else
            {
                enemyController.RandomDrop();
            }

            if(gameObject.tag == "TurretEnemy")
            {
                GameManager.Instance().AddScore();
            }
        }    

        // if(gameObject.layer == 8)
        // {
        //     Debug.Log("player layer");
        //     GameManager.Instance().EndGame();
        //     Destroy(gameObject);
        // }  
    }
}
