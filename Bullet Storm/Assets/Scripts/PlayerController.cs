using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float dashSpeed; //speed of the dash
    public int maxHealth;
    public int currentHealth;
    Vector3 velocity;

    public HealthManager healthSystem;

    public PowerUp powerUpSystem;
    public float rotationSpeed;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Rotation Implementation
            //This is very explicit
            // Grabs the rotation quaternion
        Quaternion rot = transform.rotation;
            // Grabs the Z euler angle
        float z = rot.eulerAngles.z;
            // Change the Z angle based on input
        z -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            // Recreate the quaternion 
        rot = Quaternion.Euler(0, 0, z);
            // Feed the quaternion into our rotation
        transform.rotation = rot;

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
            else
            {
                velocity = new Vector3(0, Input.GetAxis("Vertical") * Speed * Time.deltaTime, 0);
            }
        }
        else
        {   
            velocity = new Vector3(0, Input.GetAxis("Vertical") * Speed * Time.deltaTime, 0);
        }
        pos += rot * velocity;
        transform.position = pos;

        if(healthSystem.collision)
        {
            TakeDamage();
        }
        if(healthSystem.shotHealthPowerUp)
        {
            HealthBar.instance.SetHealth(currentHealth);
        }
    }

    void FixedUpdate() 
    {

    }
    void TakeDamage()
    {
        HealthBar.instance.SetHealth(currentHealth);
        healthSystem.collision = false;
    }
}
