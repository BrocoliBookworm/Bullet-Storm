using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyShooter : EnemyShooter
{
    public GameObject specialBulletHolder;
    public Transform[] specialFirePoints;
    public bool specialAvailable = false;
    public GameObject specialBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target();
        
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.position) < 15)
        {
            cooldownTimer = fireDelay;
            BossShot();

            if(specialAvailable)
            {
                SpecialShot();
            }
        }

        if(!specialAvailable)
        {
            if(gameObject.GetComponent<TankEnemyController>().currentHealth <= 15)
            {
                specialAvailable = true;
                SetSpecialShot();
            }
        }

    }

    public override void BossShot()
    {
        for(int i = 0; i < firePoints.Length; i++)
        {
            GameObject enemyBullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            enemyBullet.tag = gameObject.tag;
            Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[i].up * bulletForce, ForceMode2D.Impulse);
        } 
    }

    public void SpecialShot()
    {
        for(int i = 0; i < specialFirePoints.Length; i++)
        {
            GameObject specialBullet = Instantiate(specialBulletPrefab, specialFirePoints[i].position, specialFirePoints[i].rotation);
            specialBullet.tag = gameObject.tag;
            Rigidbody2D rb = specialBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(specialFirePoints[i].up * bulletForce, ForceMode2D.Impulse);
        }
    }

    public void SetSpecialShot()
    {
        for(int i = 0; i < 1; i++)
        {
            specialBulletHolder.SetActive(true);
        }
    }
}
