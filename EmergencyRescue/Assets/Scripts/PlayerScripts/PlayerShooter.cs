using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform centralFirepoint;
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public GameObject explosiveShotPrefab;

    public float explosiveTimer;
    private bool explosiveAvailable = false;
    public float bulletForce;

    void Start()
    {
        
    }

    void Update()
    {
        ExplosiveShotTimer();

        if(Input.GetButtonDown("Fire1"))
        {
            if(GameManager.Instance().survivors < 10)
            {
                RegularShoot();
            }
            else if(GameManager.Instance().survivors >= 10)
            {
                TripleShot();
            }
        }

        if(Input.GetButtonDown("Fire2"))
        {
            if(GameManager.Instance().survivors >= 20)
            {
                if(explosiveAvailable)
                {
                    ExplosiveShot();
                }
            }
        }
    }

    void RegularShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, centralFirepoint.position, centralFirepoint.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); //Access the bullets rigidbody2d component
        rb.AddForce(centralFirepoint.up * bulletForce, ForceMode2D.Impulse); // Puts force on the bullet

        FindObjectOfType<AudioManager>().Play("ShotSound");
    }

    void TripleShot()
    {
        
        GameObject tripleBullet = Instantiate(tripleBulletPrefab, centralFirepoint.position, centralFirepoint.rotation);
        Rigidbody2D[] rb = tripleBullet.GetComponentsInChildren<Rigidbody2D>();
        for(int i = 0; i < rb.Length; i++)
        {
            rb[i].AddForce(rb[i].transform.up * bulletForce, ForceMode2D.Impulse);
        }
        
        FindObjectOfType<AudioManager>().Play("ShotSound");
    }

    void ExplosiveShot()
    {
        GameObject explosiveBullet = Instantiate(explosiveShotPrefab, centralFirepoint.position, centralFirepoint.rotation);
        Rigidbody2D rb = explosiveBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(centralFirepoint.up * bulletForce, ForceMode2D.Impulse);

        explosiveAvailable = false;
    }

    void ExplosiveShotTimer()
    {
        if(explosiveAvailable)
        {
            explosiveTimer = 5;
        }
        else
        {
            explosiveTimer -= Time.deltaTime;
            if(explosiveTimer <= 0)
            {
                explosiveAvailable = true;
                explosiveTimer = 5;
            }
        }
    }
}
