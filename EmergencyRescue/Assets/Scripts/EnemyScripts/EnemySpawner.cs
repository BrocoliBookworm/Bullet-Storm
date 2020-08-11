using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;
    public GameObject EPrefab;

    //Timer
    float enemyRate = 5;

    //Timer to spawning enemy
    float nextEnemy = 1;

    //How far to spawn enemies 
    float spawnDistance = 25f;

    public int currentEnemies;
    private int maxEnemies = 10;

    public static EnemySpawner Instance()
    {
        if(_instance == null)
        {
            GameObject go = new GameObject("EnemySpawner"); //assign instance to this instance of the class
            go.AddComponent<GameManager>();
        }

        return _instance;
    }

    void Awake() 
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance().survivorsSaved < 30)
        {
            if(currentEnemies < maxEnemies)
            {
                Debug.Log("max is 10");
                Spawn();
            }
            else
            {
                Debug.Log("max");
                return;
            }
        }
        else if(GameManager.Instance().survivorsSaved > 50)
        {
            if(GameManager.Instance().survivorsSaved > 75)
            {
                maxEnemies = 50;

                if(currentEnemies < maxEnemies)
                {
                    Debug.Log("max is 50");
                    Spawn();
                }
                else
                {
                    Debug.Log("max");
                    return;
                }   
            }
            else
            {
                maxEnemies = 25;

                if(currentEnemies < maxEnemies)
                {
                    Debug.Log("max is 25");
                    Spawn();
                }
                else
                {
                    Debug.Log("max");
                    return;
                }      
            }  
        }
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
                enemyRate = 1.5f;
            }

            //Creates an offset in a random area in a sphere
            Vector3 offset = Random.onUnitSphere;

            offset.z = 0;
            
            offset = offset.normalized * spawnDistance;

            //spawns the enemies
            Instantiate(EPrefab, transform.position + offset, Quaternion.identity);
            currentEnemies++;
            Debug.Log("Current Enemies: " + currentEnemies);
        }
    }
}
