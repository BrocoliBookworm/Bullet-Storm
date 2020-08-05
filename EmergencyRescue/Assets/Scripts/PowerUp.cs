using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject healthPickupEffect;
    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.GetComponent<PlayerController>())
       {
           Pickup(other);
       }
   }

   void Pickup(Collider2D player)
   {
        //Spawn effect
        var clone = Instantiate(healthPickupEffect, transform.position, transform.rotation);

        //Remove power up object
        Destroy(clone, 2f);
        Destroy(gameObject);
   }
}
