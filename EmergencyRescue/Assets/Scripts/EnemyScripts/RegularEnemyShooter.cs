using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemyShooter : EnemyShooter
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
            BasicEnemyShot();
        }
    }

    public override void BasicEnemyShot()
    {
        GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>(); 
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); 
    }
}
