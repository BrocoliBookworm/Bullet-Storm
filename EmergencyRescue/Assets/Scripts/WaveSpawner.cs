using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum spawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform survivor;
        public Transform kidnapEnemy;
        public Transform turretEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] survivorSpawnPoints;
    public Transform[] kidnapEnemySpawnPoints;
    public Transform[] turretEnemySpawnPoints;

    public float survivorSpawnDistance;
    public float turretSpawnDistance;



    private float timerBetweenWaves = 10f;
    private float waveCountDown;

    private spawnState state = spawnState.COUNTING;

    void Start()
    {
        waveCountDown = timerBetweenWaves;

        if(survivorSpawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        
        if(kidnapEnemySpawnPoints.Length == 0)
        {
            Debug.LogError("No kidnap enemy spawn points referenced");
        }

        if(turretEnemySpawnPoints.Length == 0)
        {
            Debug.LogError("No turret enemy spawn points referenced");
        }
    }

    void Update()
    {
        if(state == spawnState.WAITING)
        {
            WaveCompleted();
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
        // Debug.Log("Spawning wave: " + _wave.name);
        state = spawnState.SPAWNING;

        Transform _sp = survivorSpawnPoints[Random.Range(0, survivorSpawnPoints.Length)];
        Transform _spForTurret = turretEnemySpawnPoints[Random.Range(0, turretEnemySpawnPoints.Length)];
            
        // Spawn
        for(int i = 0; i < _wave.count; i++)
        {

            SpawnSurvivor(_wave.survivor, _sp);
            SpawnTurretEnemy(_wave.turretEnemy, _spForTurret);
            SpawnKidnapEnemy(_wave.kidnapEnemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = spawnState.WAITING;

        yield break;
    }

    void WaveCompleted()
    {
        state = spawnState.COUNTING;
        waveCountDown = timerBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            // Debug.Log("ALL WAVES COMPLETE, Looping");
        }
        else
        {
            nextWave++;
        }
    }

    void SpawnSurvivor(Transform survivor, Transform spawnlocation)
    {
        // Transform _sp = survivorSpawnPoints[Random.Range(0, survivorSpawnPoints.Length)];

        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawn survivors
        Instantiate(survivor, spawnlocation.position + offset, spawnlocation.rotation);
    }

    void SpawnTurretEnemy(Transform turret, Transform spawnlocation)
    {
        // Transform _sp = turretEnemySpawnPoints[Random.Range(0, turretEnemySpawnPoints.Length)];

        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawning
        Instantiate(turret, spawnlocation.position + offset, spawnlocation.rotation);
    }

    void SpawnKidnapEnemy(Transform kidnapEnemy)
    {
        Transform _sp = kidnapEnemySpawnPoints[Random.Range(0, kidnapEnemySpawnPoints.Length)];
        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawn enemies
        Instantiate(kidnapEnemy, _sp.position + offset, _sp.rotation);
    }
}
