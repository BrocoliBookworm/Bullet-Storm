using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidnapEnemyController : MonoBehaviour
{
    public float rotationSpeed;
    public Transform survivor;
    public Transform target;
    public Transform warpEastTarget;
    public Transform warpWestTarget;
    public float speed;
    public GameObject survivorObject;
    public Transform spawnPoint;

    public bool success = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;

        if(survivor == null)
        {
            if(success != true)
            {
                target = GameObject.FindWithTag("Survivor").transform;

                if(target != null)
                {
                    // Debug.Log("survivor");
                    survivor = target.transform;
                }
            }

            if(success == true)
            {
                // Debug.Log("success is true");
                if(Random.value > 0.5)
                {
                    // Debug.Log("east");
                    survivor = warpEastTarget.transform;
                }
                else
                {
                    // Debug.Log("west");
                    survivor = warpWestTarget.transform;
                }
            }
        }
        
        if(survivor == null)
        {
            return;
        }

        Vector3 dir = survivor.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation,desiredRot, rotationSpeed * Time.deltaTime);
    }

    public void SurvivorDrop()
    {
        Instantiate(survivorObject, spawnPoint.position, spawnPoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == 15)
        {
            if(success)
            {
                Destroy(gameObject);
            }
        }
    }
}
