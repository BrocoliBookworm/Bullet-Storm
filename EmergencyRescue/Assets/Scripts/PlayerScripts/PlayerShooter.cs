using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform centralFirepoint;
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public GameObject explosiveShotPrefab;

    public float reloadTimer = 0.1f;

    public bool shotAvailable = true;

    private float explosiveTimer = 5f;
    private bool explosiveAvailable = false;
    public float bulletForce;

    void Start()
    {
        
    }

    void Update()
    {
        ExplosiveShotTimer();
        Reload();

        if(Input.GetButton("Fire1"))
        {
            if(shotAvailable)
            {
                if(GameManager.Instance().onShipSurvivors < 10)
                {
                    RegularShoot();
                    shotAvailable = false;
                }
                else if(GameManager.Instance().onShipSurvivors >= 10)
                {
                    TripleShot();
                    shotAvailable = false;
                }
            }
        }

        if(Input.GetButton("Fire2"))
        {
            if(GameManager.Instance().onShipSurvivors >= 20)
            {
                if(explosiveAvailable)
                {
                    ExplosiveShot();
                }
            }
        }
    }

    public void Reload()
    {
        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0)
        {
            reloadTimer = 0.1f;
            shotAvailable = true;
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

        FindObjectOfType<AudioManager>().Play("PowerUpShot");
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

    public virtual void AssistShot()
    {

    }
}
