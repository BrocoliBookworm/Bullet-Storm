using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the order of enemies’, bonuses and background objects’ emerging. 
/// </summary>
#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

[System.Serializable]
public class Bonuses 
{
    [Tooltip("Bonuses prefab")]
    public GameObject levelUp;
    public GameObject [] weaponBonus;

    [Tooltip("time interval between bonus generation")]
    public float timeForNewPowerup, timeForNewWeapon;
}

[System.Serializable]
public class BackgroundPlanets
{
    [Tooltip("Prefab of the planets' parent object")]
    public GameObject[] planets;
    public float speed;
    [Tooltip("The time between the appearances of compilations")]
    public float timeBetween;
}
#endregion

public class LevelController : MonoBehaviour {

    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 
    public Bonuses bonuses; 
    public BackgroundPlanets backgroundPlanets;

    Camera mainCamera;

    //List for planets and bonuses arrays. The list will decrease with the new objects appearance and after reaching zero will be installed. This will avoid objects repetition 
    List<GameObject> planetsList = new List<GameObject>();
    public List<GameObject> bonusesList = new List<GameObject>();

    private void Start()
    {
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        for (int i = 0; i<enemyWaves.Length; i++) 
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
        StartCoroutine(PowerupBonusCreation()); 
        StartCoroutine(BackgroundPlanetsCreation());
        StartCoroutine(NewWeaponBonusCreation());
    }
    
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(bonuses.timeForNewPowerup);
            if (Player.instance != null)
            {
                Instantiate(
                    bonuses.levelUp,
                    //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                    new Vector2(
                        Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                        mainCamera.ViewportToWorldPoint(Vector2.up).y + bonuses.levelUp.GetComponent<Renderer>().bounds.size.y / 2),
                    Quaternion.identity
                    );
            }
        }
    }

    IEnumerator NewWeaponBonusCreation() 
    {
        //Create a new list copying bonuses arrey
        for (int i = 0; i < bonuses.weaponBonus.Length; i++)
        {
            bonusesList.Add(bonuses.weaponBonus[i]);
        }

        //with the defined intervals create 'new weapon bonuses'
        while (true) 
        {
            //choose random bonus from the list, generate and delete it
            int randomIndex = Random.Range(0, bonusesList.Count);
            yield return new WaitForSeconds(bonuses.timeForNewWeapon);
            GameObject newBonus = bonusesList[randomIndex];
            bonusesList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (bonusesList.Count==0)
            {
                for (int i = 0; i < bonuses.weaponBonus.Length; i++)
                {
                    bonusesList.Add(bonuses.weaponBonus[i]);
                }
            }
            Instantiate(
                newBonus,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + newBonus.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator BackgroundPlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i< backgroundPlanets.planets.Length; i++)
        {
            planetsList.Add(backgroundPlanets.planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count==0)
            {
                for (int i = 0; i < backgroundPlanets.planets.Length; i++)
                {
                    planetsList.Add(backgroundPlanets.planets[i]);
                }
            }
            newPlanet.GetComponent<Planets_Parent>().speed = backgroundPlanets.speed;
            
            yield return new WaitForSeconds(backgroundPlanets.timeBetween);
        }
        

    }
}
