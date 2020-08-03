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

    float invulnerableTimer = 0;

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
        if(gameObject.tag == other.tag)
        {
            return;
        }

        if(other.gameObject.layer == 11)
        {
            HealthBoost();
        }
        else if(other.gameObject.layer == 12)
        {
            GameManager.Instance().RescueSurvivors();
        }
        else if(other.gameObject.layer == 14)
        {
            GameManager.Instance().AddSurvivors();
        }
        else
        {
            TakeDamage();
        }
    }

    public override void HealthBoost()
    {
        FindObjectOfType<AudioManager>().Play("PowerUpSound");
        currentHealth++;
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

    public override void TakeDamage()
    {
        currentHealth--;

        invulnerableTimer = 0.5f;

        gameObject.layer = 10;

        HealthBar.instance.SetHealth(currentHealth);

        Invulnerability();
    }

    public override void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        GameManager.Instance().EndGame();
    }
}
