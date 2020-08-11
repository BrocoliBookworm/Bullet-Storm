using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HealthManager
{
    public float speed;
    public int maxHealth;
    public float dashSpeed;

    private float survivorAtTen = 0.75f;

    private float survivorAtTwenty = 0.5f;

    public float invulnerableTimer = 0;

    // public GameObject deathEffect;

    public GameObject[] hurtEffects;

    public GameObject[] assistShips;

    Vector3 velocity;
    public float maxHeight;
    public float minHeight;
    public float leftDistance;
    public float rightDistance;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        FaceMouse();
        Movement();       
        CheckBoundaries();
        Invulnerability();
        //DamageEffect();

        if(GameManager.Instance().assistShipSurvivorSaved >= 5)
        {
            Debug.Log("5");
            assistShips[0].SetActive(true);
        }

        if(GameManager.Instance().assistShipSurvivorSaved >= 10)
        {
            Debug.Log("10");
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
        // else if(GameManager.Instance().survivors >= 10)
        // {
        //     velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        //     velocity = velocity.normalized * speed * survivorAtTen * Time.deltaTime;
        //     // Debug.Log("10");
        // }
        // else if(GameManager.Instance().survivors >= 20)
        // {
        //     velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        //     velocity = velocity.normalized * speed * survivorAtTwenty * Time.deltaTime;
        //     // Debug.Log("20");
        // }
        else
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            velocity = velocity.normalized * speed * Time.deltaTime;
            // Debug.Log("normal");
        }

        // _velocity += velocity * Time.deltaTime;
        // var magnitude = _velocity.magnitude;
        // if(magnitude > speed)
        // {
        //     _velocity *= speed / magnitude;
        // }

        // pos += _velocity;
        pos += velocity;
        transform.position = pos;
    }

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
        else if(other.GetComponent<Bullet>() || other.GetComponent<KidnapEnemyController>() || other.GetComponent<BasicEnemyController>() || other.GetComponent<TurretEnemyController>())
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
            GameManager.Instance().RescueSurvivors();
            return;
        }
        
        if(other.GetComponent<SurvivorController>())
        {
            if(other.GetComponent<OnShipSurvivorsController>())
            {
                if(GameManager.Instance().survivors <= 29)
                {
                    GameManager.Instance().onShipSurvivors++;
                }
                else if(GameManager.Instance().survivors >= 30)
                {
                    return;
                }
            }

            if(other.GetComponent<AssistShipSurvivorsController>())
            {
                if(GameManager.Instance().survivors <= 29)
                {
                    GameManager.Instance().assistShipSurvivors++;
                }
                else if(GameManager.Instance().survivors >= 30)
                {
                    return;
                }
            }

            if(GameManager.Instance().survivors <= 29)
            {
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
            var clone = Instantiate(hurtEffects[0], transform.position, transform.rotation);
            Debug.Log("small");
        }

        if(currentHealth <= 5)
        {
            var clone = Instantiate(hurtEffects[1], transform.position, transform.rotation);
            Debug.Log("medium");
        }
        
        if(currentHealth <= 3)
        {
            var clone = Instantiate(hurtEffects[2], transform.position, transform.rotation);
            Debug.Log("large");
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
