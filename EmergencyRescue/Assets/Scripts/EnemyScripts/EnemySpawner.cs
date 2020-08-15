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
    private int enemyCap = 50;

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
        if(GameManager.Instance().survivorsSaved <= 30)
        {
            enemyCap = 10;

            if(currentEnemies < enemyCap)
            {
                Spawn();
            }
            else
            {
                return;
            }
        }
        else if(GameManager.Instance().survivorsSaved > 31)
        {
            if(GameManager.Instance().survivorsSaved >= 60 && GameManager.Instance().survivorsSaved <= 90)
            {
                enemyCap = 15;

                if(currentEnemies < enemyCap)
                {
                    Spawn();
                }
                else
                {
                    return;
                }   
            }
            else if(GameManager.Instance().survivorsSaved > 90 && GameManager.Instance().survivorsSaved <= 120)
            {
                enemyCap = 25;

                if(currentEnemies < enemyCap)
                {
                    Spawn();
                }
                else
                {
                    return;
                }   
            }
            else if(GameManager.Instance().survivorsSaved > 120 && GameManager.Instance().survivorsSaved <= 150)
            {
                enemyCap = 30;

                if(currentEnemies < enemyCap)
                {
                    Spawn();
                }
                else
                {
                    return;
                }   
            }
            else if(GameManager.Instance().survivorsSaved > 150 && GameManager.Instance().survivorsSaved <= 180)
            {
                enemyCap = 38;

                if(currentEnemies < enemyCap)
                {
                    Spawn();
                }
                else
                {
                    return;
                }   
            }
            else if(GameManager.Instance().survivorsSaved > 180)
            {
                enemyCap = 50;

                if(currentEnemies < enemyCap)
                {
                    Spawn();
                }
                else
                {
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
                if(GameManager.Instance().survivorsSaved >= 66)
                {
                    enemyRate = 1f;

                    if(GameManager.Instance().survivorsSaved >= 132)
                    {
                        enemyRate = 0.5f;
                    }
                }
                else
                {
                    enemyRate = 1.5f;
                }
            }

            //Creates an offset in a random area in a sphere
            Vector3 offset = Random.onUnitSphere;

            offset.z = 0;
            
            offset = offset.normalized * spawnDistance;

            //spawns the enemies
            Instantiate(EPrefab, transform.position + offset, Quaternion.identity);
            currentEnemies++;
        }
    }
}
