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

    public Transform player;
    public float cooldownTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Target()
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
    }

    public virtual void BasicEnemyShot()
    {

    }

    public virtual void TurretShot()
    {

    }

    public virtual void BossShot()
    {
        
    }
}
