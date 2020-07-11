using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float fireDelay;

    Transform player;
    float cooldownTimer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            //Find the players ship
            GameObject go = GameObject.FindWithTag("Player"); //go = GameObject
            
            if(go != null)
            {
                player = go.transform;
            }
        }

        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.position) < 10)
        {
            cooldownTimer = fireDelay;

            GameObject Enemybullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Creates the bullet
            Rigidbody2D rb = Enemybullet.GetComponent<Rigidbody2D>(); //Access the bullets rigidbody2d component
            rb.AddForce(firePoint.up * bulletForce,ForceMode2D.Impulse); // Puts force on the bullet        
        }
    }
}
