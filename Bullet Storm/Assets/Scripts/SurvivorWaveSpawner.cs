using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorWaveSpawner : MonoBehaviour
{
    public enum spawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform survivor;
        public Transform kidnapEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timerBetweenWaves = 10f;
    public float waveCountDown;

    private spawnState state = spawnState.COUNTING;

    void Start()
    {
        waveCountDown = timerBetweenWaves;
    }

    void Update()
    {
        if(state == spawnState.WAITING)
        {
            //Run fixed timer
            Debug.Log("In Wait");
            waveCountDown = timerBetweenWaves;
            waveCountDown -= Time.deltaTime;
        }

        if(waveCountDown <= 0)
        {
           if(state != spawnState.SPAWNING)
           {
               StartCoroutine(spawnWave(waves[nextWave]));
           }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    //Sadly I must use the devil
    IEnumerator spawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = spawnState.SPAWNING;

        // Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnSurvivor(_wave.survivor);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = spawnState.WAITING;

        yield break;
    }

    void SpawnSurvivor(Transform survivor)
    {
        //Spawn survivors
        Instantiate(survivor, transform.position, transform.rotation);
        Debug.Log("Spawn survivor" + survivor.name);
    }

    void SpawnKidnapEnemy(Transform kidnapEnemy)
    {
        //Spawn enemies
        Debug.Log("Spawn Kidnap Enemy: " + kidnapEnemy.name);
    }
}
