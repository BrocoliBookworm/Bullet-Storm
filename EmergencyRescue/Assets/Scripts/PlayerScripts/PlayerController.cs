using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HealthManager
{
    private float speed;
    private float maxSpeed = 15f;
    private float percentCalc; //Finds the ratio of survivors multiplied by 100 
    private float newPercent; //Calculates the percentage of speed available to move at with the current survivors on board 
    private int maxHealth = 10;   //public int maxHealth { get; private set; }
    private float dashSpeed = 200f;

    private float invulnerableTimer = 0;

    public GameObject[] hurtEffects;

    public GameObject[] assistShips;

    Vector3 velocity;
    private float maxHeight = 44f;
    private float minHeight = -65.5f;
    private float leftDistance = -52.6f;
    private float rightDistance = 58.1f;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        FaceMouse();

        if(GameManager.Instance().survivors == 0)
        {
            speed = maxSpeed;
        }
        else
        {
            percentCalc = GameManager.Instance().survivors / 30f; //take out 100 and change 1 - percentCalc to 1
            newPercent = 1 - percentCalc;

            speed = maxSpeed * newPercent;
        }

        if(speed < 10)
        {
            speed = 10;
        }

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
                
        // CalculateSpeed();
        Movement();       
        CheckBoundaries();
        Invulnerability();
        DamageEffect();

        if(GameManager.Instance().survivorsSaved >= 66)
        {
            assistShips[0].SetActive(true);
        }

        if(GameManager.Instance().survivorsSaved >= 132)
        {
            assistShips[1].SetActive(true);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    Vector3 _velocity;

    public void Movement()
    {
        Vector3 pos = transform.position;

        if(Input.GetButtonDown("Fire3"))
        {
            if(StaminaBar.instance.equalized)
            {
                StaminaBar.instance.UseStamina();
                if(Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") == 0)
                {
                    velocity = new Vector3(0, Input.GetAxis("Vertical") * dashSpeed * Time.deltaTime, 0);
                }
                else if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
                {
                    velocity = new Vector3(Input.GetAxis("Horizontal") * dashSpeed * Time.deltaTime, 0, 0);
                }
                else if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
                {
                    velocity = new Vector3(Input.GetAxis("Horizontal") * dashSpeed * Time.deltaTime, Input.GetAxis("Vertical") * dashSpeed * Time.deltaTime, 0);
                }
            }
        }
        else
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            velocity = velocity.normalized * speed * Time.deltaTime;
        }

        pos += velocity;
        transform.position = pos;
    }

    // void CalculateSpeed()
    // {
    //     if(GameManager.Instance().survivors == 0)
    //     {
    //         speed = maxSpeed;
    //         Debug.Log("set to max");
    //     }
    //     else
    //     {
    //         percentCalc = GameManager.Instance().survivors / 30 * 100;
    //         Debug.Log("Percent Calculation: " + percentCalc);
    //         newPercent = (100 - percentCalc)/100;
    //         Debug.Log("New Percent: " + newPercent);

    //         speed = maxSpeed * newPercent;
    //         Debug.Log("Speed: " + speed);
    //     }

    //     if(speed < 10)
    //     {
    //         speed = 10;
    //     }

    //     if(speed > maxSpeed)
    //     {
    //         speed = maxSpeed;
    //     }
        
    //     Debug.Log("done calculating");
    // }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

    public void CheckBoundaries()
    {
        if(transform.position.y > maxHeight || transform.position.y < minHeight || transform.position.x > rightDistance || transform.position.x < leftDistance)
        {
            BorderControl.instance.EnterArea();
        }
        else if(transform.position.y < maxHeight || transform.position.y > minHeight || transform.position.x < rightDistance || transform.position.x > leftDistance)
        {
            BorderControl.instance.ExitArea();
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(gameObject.layer == other.gameObject.layer)
        {
            return;
        }
        else if(other.GetComponent<Bullet>() || other.GetComponent<KidnapEnemyController>() || other.GetComponent<TankEnemyController>() || other.GetComponent<BasicEnemyController>() || other.GetComponent<TurretEnemyController>())
        {
            TakeDamage(1);
            return;
        }

        if(other.GetComponent<PowerUp>())
        {
            HealthBoost();
            return;
        }
        
        if(other.GetComponent<GameManager>())
        {
            if(GameManager.Instance().survivors >= 1)
            {
                GameManager.Instance().RescueSurvivors();
            }
        }
        
        if(other.GetComponent<SurvivorController>())
        {
            if(other.GetComponent<OnShipSurvivorsController>())
            {
                if(GameManager.Instance().survivors <= 29)
                {
                    FindObjectOfType<AudioManager>().Play("SurvivorRescue");
                    GameManager.Instance().onShipSurvivors++;
                }
                else if(GameManager.Instance().survivors >= 30)
                {
                    return;
                }
            }

            if(GameManager.Instance().survivors <= 29)
            {
                FindObjectOfType<AudioManager>().Play("SurvivorRescue");
                GameManager.Instance().AddSurvivors();
            }
            else if(GameManager.Instance().survivors >= 30)
            {
                return;
            }

            return;
        }

        if(other.gameObject.layer == 15)
        {
            return;
        }
    }

    public override void HealthBoost()
    {
        FindObjectOfType<AudioManager>().Play("PowerUpSound");
        if(currentHealth < 10)
        {
            currentHealth++;
        }
        HealthBar.instance.SetHealth(currentHealth);
    }

    public override void Invulnerability()
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
    }

    public override void TakeDamage(int damageTaken)
    {
        currentHealth = currentHealth - damageTaken;

        invulnerableTimer = 0.5f;

        gameObject.layer = 10;

        HealthBar.instance.SetHealth(currentHealth);
    }

    public void DamageEffect()
    {
        if(currentHealth <= 7)
        {
            hurtEffects[0].SetActive(true);

            if(currentHealth <= 5)
            {
                hurtEffects[1].SetActive(true);

                if(currentHealth <= 3)
                {
                    hurtEffects[2].SetActive(true);
                }
                else if(currentHealth > 3 && currentHealth <= 5)
                {
                    hurtEffects[2].SetActive(false);
                }
            }
            else if(currentHealth > 5 && currentHealth <= 7)
            {
                hurtEffects[1].SetActive(false);
            }
        }
        else if(currentHealth > 7)
        {
            hurtEffects[0].SetActive(false);
        }
        
        if(currentHealth == 0)
        {
            return;
        }
    }

    public override void Die()
    {
        // var clone = Instantiate(deathEffect, transform.position, transform.rotation);
        
        // Destroy(clone, 1f);
        // FindObjectOfType<AudioManager>().Play("PlayerDeath");
        // Invoke("GameOver", 4f);
        // Destroy(gameObject, 4f);

        GameManager.Instance().EndGame();
    }

    // public void GameOver()
    // {
    //     GameManager.Instance().EndGame();
    // }
}
