using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private static WaveSpawner _instance;

    public enum spawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform survivor;
        public Transform onShipSurvivors;
        public Transform kidnapEnemy;
        public Transform turretEnemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] survivorSpawnPoints;
    public bool survivorSpawn1;
    public bool survivorSpawn2;
    public bool survivorSpawn3;
    public bool survivorSpawn4;
    public Transform[] kidnapEnemySpawnPoints;
    public Transform[] turretEnemySpawnPoints;

    public float survivorSpawnDistance;
    public float turretSpawnDistance;
    private float timerBetweenWaves = 10f;
    private float waveCountDown;

    private Transform _sp;

    private spawnState state = spawnState.COUNTING;

    public static WaveSpawner Instance()
    {
        if(_instance == null)
        {
            GameObject go = new GameObject("WaveSpawner");
            go.AddComponent<WaveSpawner>();
        }

        return _instance;
    }

    void Awake() 
    {
        _instance = this;
    }


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
        state = spawnState.SPAWNING;

        _sp = survivorSpawnPoints[Random.Range(0, survivorSpawnPoints.Length)];

        if(_sp == survivorSpawnPoints[0])
        {
            survivorSpawn1 = true;
        }
        else
        {
            survivorSpawn1 = false;
        }

        if(_sp == survivorSpawnPoints[1])
        {
            survivorSpawn2 = true;
        }
        else
        {
            survivorSpawn2 = false;
        }

        if(_sp == survivorSpawnPoints[2])
        {
            survivorSpawn3 = true;
        }
        else
        {
            survivorSpawn3 = false;
        }

        if(_sp == survivorSpawnPoints[3])
        {
            survivorSpawn4 = true;
        }
        else
        {
            survivorSpawn4 = false;
        }
        
            
        // Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnSurvivor(_wave.survivor, _sp);
            SpawnOnShipSurvivor(_wave.onShipSurvivors, _sp);
            SpawnTurretEnemy(_wave.turretEnemy);
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
        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawn survivors
        Instantiate(survivor, spawnlocation.position + offset, spawnlocation.rotation);
    }

    void SpawnOnShipSurvivor(Transform onShipSurvivors, Transform spawnlocation)
    {
        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawn survivors
        Instantiate(onShipSurvivors, spawnlocation.position + offset, spawnlocation.rotation);
    }

    void SpawnTurretEnemy(Transform turret)
    {
        Transform _spturret = turretEnemySpawnPoints[Random.Range(0, turretEnemySpawnPoints.Length)];

        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * turretSpawnDistance;

        //Spawning
        Instantiate(turret, _spturret.position + offset, _spturret.rotation);
    }

    void SpawnKidnapEnemy(Transform kidnapEnemy)
    {
        Transform _spkidnap = kidnapEnemySpawnPoints[Random.Range(0, kidnapEnemySpawnPoints.Length)];
        Vector3 offset = Random.onUnitSphere;
        offset.z = 0;

        offset = offset.normalized * survivorSpawnDistance;

        //Spawn enemies
        Instantiate(kidnapEnemy, _spkidnap.position + offset, _spkidnap.rotation);
    }
}
