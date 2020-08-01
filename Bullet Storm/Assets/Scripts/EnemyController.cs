using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float rotationSpeed;
    public Transform player;

    public Transform target;
    public float speed;

    public GameObject powerUp;
    public Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        if(player == null)
        {
            //Find the players ship
            target = GameObject.FindWithTag("Player").transform;
            
            if(target != null)
            {
                player = target.transform;
            }
        }

        //We've either found the player or the player doesn't exist rn
        
        if(player == null)
        {
            return; //Try next frame
        }

        //Here we know there is a player. Go get it
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotationSpeed * Time.deltaTime);
    }

    public void RandomDrop()
    {
        if(Random.value > 0.7)
        {
            Instantiate(powerUp, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
