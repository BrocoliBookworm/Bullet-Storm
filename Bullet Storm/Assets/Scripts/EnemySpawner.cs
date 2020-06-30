using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EPrefab;
    float enemyRate = 5;
    float nextEnemy = 1;
    float spawnDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nextEnemy -= Time.deltaTime;

        if(nextEnemy <= 0)
        {
            nextEnemy = enemyRate;
            enemyRate *= 0.9f;
            if(enemyRate < 2)
            {
                enemyRate = 2;
            }

            Vector3 offset = Random.onUnitSphere;

            offset.z = 0;
            
            offset = offset.normalized * spawnDistance;

            Instantiate(EPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
