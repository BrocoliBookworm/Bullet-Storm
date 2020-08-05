using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HealthManager
{
    public float speed;
    public int maxHealth;
    public float dashSpeed; //speed of the dash

    private float survivorAtTen = 0.75f;

    private float survivorAtTwenty = 0.5f;

    public float invulnerableTimer = 0;

    public GameObject deathEffect;

    Vector3 velocity;
    // private Rigidbody2D rb2d;
    public float maxHeight;
    public float minHeight;
    public float leftDistance;
    public float rightDistance;

    // public Vector2 movement;

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

        if(currentHealth <= 0)
        {
            Debug.Log("died");
            Die();
        }
    }

    public void Movement()
    {
        Vector3 pos = transform.position;
        

        //If you press the jump key you should get a speed boost
        if(Input.GetButtonDown("Fire3"))
        {
            if(StaminaBar.instance.equalized)
            {
                StaminaBar.instance.UseStamina();
                velocity = new Vector3(0, Input.GetAxis("Vertical") * dashSpeed * Time.deltaTime, 0);
                Debug.Log("dash");
            }
        }
        else if(GameManager.Instance().survivors >= 10)
        {
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            velocity = velocity.normalized * speed * survivorAtTen * Time.deltaTime;
            // Debug.Log("10");
        }
        else if(GameManager.Instance().survivors >= 20)
        {
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            velocity = velocity.normalized * speed * survivorAtTwenty * Time.deltaTime;
            // Debug.Log("20");
        }
        else
        {
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            velocity = velocity.normalized * speed * Time.deltaTime;
            // Debug.Log("normal");
        }
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
        else if(other.GetComponent<Bullet>())
        {
            TakeDamage(1);
            return;
        }

        if(other.GetComponent<PowerUp>())
        {
            HealthBoost();
            return;
        }
        
        if(other.gameObject.layer == 12)
        {
            GameManager.Instance().RescueSurvivors();
            return;
        }
        
        if(other.GetComponent<SurvivorController>())
        {
            GameManager.Instance().AddSurvivors();
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
            Debug.Log("greater than 0");
            invulnerableTimer -=Time.deltaTime;
        
            if(invulnerableTimer <= 0)
            {
                Debug.Log("got there");
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

    public override void Die()
    {
        var clone = Instantiate(deathEffect, transform.position, transform.rotation);
        
        Destroy(clone, 1f);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Invoke("GameOver", 4f);
        Destroy(gameObject);
        Debug.Log("invoked");
        // GameManager.Instance().EndGame();
    }

    void GameOver()
    {
        Debug.Log("end called");
        GameManager.Instance().EndGame();
    }
}
