using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public GameObject turretBulletPrefab;
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
            if(gameObject.tag == "Enemy")
            {
                GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Creates the bullet
                Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>(); //Access the bullets rigidbody2d component
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); // Puts force on the bullet   
            }
            else if(gameObject.tag == "TurretEnemy")
            {
                for(int i = 0; i < firePoints.Length; i++)
                {
                    Vector3 v = (firePoints[i].transform.position - transform.position).normalized;
                    Quaternion rot = Quaternion.FromToRotation(Vector3.up, v); //research this
                    GameObject enemyBullet = Instantiate(bulletPrefab, firePoints[i].position, rot);
                    enemyBullet.tag = gameObject.tag;
                    enemyBullet.GetComponent<Bullet>().velocity = v * bulletForce;
                } 
                // GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // enemyBullet.tag = gameObject.tag;
                // enemyBullet.GetComponent<Bullet>().velocity = (player.position - transform.position).normalized * bulletForce;

                // Rigidbody2D[] rb = enemyBullet.GetComponentsInChildren<Rigidbody2D>();
                // for(int i = 0; i < rb.Length; i++)
                // {
                //     //rb[i].AddForce((transform.position - player.position).normalized * bulletForce, ForceMode2D.Impulse);
                // }
            }
        }
    }
}
