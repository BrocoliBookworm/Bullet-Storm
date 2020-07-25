using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dashSpeed; //speed of the dash
    public int maxHealth;
    public int currentHealth;
    Vector3 velocity;
    private Rigidbody2D rb2d;

    private Camera mainCamera;

    public HealthManager healthSystem;

    public PowerUp powerUpSystem;
    public float rotationSpeed;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
        rb2d = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        FaceMouse();

        // Movement Implementation
            // Returns a float from -1 to 1
        Vector3 pos = transform.position;
        

        //If you press the jump key you should get a speed boost
        if(Input.GetButtonDown("Fire2"))
        {
            if(StaminaBar.instance.equalized)
            {
                StaminaBar.instance.UseStamina();
                velocity = new Vector3(0, Input.GetAxis("Vertical") * dashSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            velocity = velocity.normalized * speed * Time.deltaTime;
        }
        pos += velocity;
        transform.position = pos;
        

        if(healthSystem.collision)
        {
            TakeDamage();
        }
        if(healthSystem.shotHealthPowerUp)
        {
            HealthBar.instance.SetHealth(currentHealth);
            healthSystem.shotHealthPowerUp = false;
        }
    }

    void TakeDamage()
    {
        HealthBar.instance.SetHealth(currentHealth);
        healthSystem.collision = false;
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
