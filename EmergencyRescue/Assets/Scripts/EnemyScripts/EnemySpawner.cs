using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EPrefab;

    //Timer
    float enemyRate = 5;

    //Timer to spawning enemy
    float nextEnemy = 1;

    //How far to spawn enemies 
    float spawnDistance = 25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       Spawn();
    }

    void Spawn()
    {
        nextEnemy -= Time.deltaTime;

        if(nextEnemy <= 0)
        {
            nextEnemy = enemyRate;
            enemyRate *= 0.9f;
            //Sets a bare minimum timer for spawning in enemies
            if(enemyRate < 2)
            {
                enemyRate = 2;
            }

            //Creates an offset in a random area in a sphere
            Vector3 offset = Random.onUnitSphere;

            offset.z = 0;
            
            offset = offset.normalized * spawnDistance;

            //spawns the enemies
            Instantiate(EPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
