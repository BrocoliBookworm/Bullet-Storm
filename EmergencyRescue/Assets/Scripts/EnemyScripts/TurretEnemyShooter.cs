using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyShooter : EnemyShooter
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
            TurretShot();
        }
    }

    public override void TurretShot()
    {
        for(int i = 0; i < firePoints.Length; i++)
        {
            Vector3 v = (firePoints[i].transform.position - transform.position).normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, v); //research this
            GameObject enemyBullet = Instantiate(bulletPrefab, firePoints[i].position, rot);
            enemyBullet.tag = gameObject.tag;
            enemyBullet.GetComponent<Bullet>().velocity = v * bulletForce;
        } 
    }
}
