using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyShooter : EnemyShooter
{
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
        }

    }

    public override void BossShot()
    {
        for(int i = 0; i < firePoints.Length; i++)
        {
            Vector3 v = (firePoints[i].transform.position - transform.position).normalized;
            // Quaternion rot = Quaternion.FromToRotation(Vector3.up, v); //research this
            GameObject enemyBullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            enemyBullet.tag = gameObject.tag;
            Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoints[i].up * bulletForce, ForceMode2D.Impulse);
        } 
    }

    // public override void BasicEnemyShot()
    // {
    //     GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //     Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>(); 
    //     rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); 
    // }
}
