using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(gameObject.tag == other.tag)
        {
            return;
        }

        if(gameObject.GetComponent<ExplodingBullet>())
        {
            if(other.GetComponent<KidnapEnemyController>() || other.GetComponent<TankEnemyController>() || other.GetComponent<BasicEnemyController>() || other.GetComponent<TurretEnemyController>())
            {
                ExplodeDestroy();
            }
        }

        if(gameObject.GetComponent<BossBullet>())
        {
            if(other.GetComponent<PlayerController>())
            {
                ExplodeDestroy();
            }
        }

        if(other.GetComponent<KidnapEnemyController>() || other.GetComponent<TankEnemyController>() || other.GetComponent<BasicEnemyController>() || other.GetComponent<TurretEnemyController>() || other.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }

        if(other.gameObject.layer == 15)
        {
            return;
        }

        if(other.gameObject.layer == 12)
        {
            return;
        }

        if(other.GetComponent<PowerUp>())
        {
            return;
        }
    }

    public virtual void ExplodeDestroy()
    {

    }
}
