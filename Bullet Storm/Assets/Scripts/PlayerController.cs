﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float dashSpeed; //speed of the dash
    public int maxHealth = 1;
    public int currentHealth;
    Vector3 velocity;

    public HealthBar healthBar;
    public HealthManager healthSystem;
    public float rotationSpeed;
    //public Rigidbody2D rb;
    //Vector2 movement;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //staminaBar.SetMaxStamina(maxRecharge);
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
            if(StaminaBar.instance.regen == null)
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
    }

    void FixedUpdate() 
    {

    }

    void TakeDamage()
    {
        currentHealth -= healthSystem.collisionDamage;
        healthBar.SetHealth(currentHealth);
        healthSystem.collision = false;
    }
}
