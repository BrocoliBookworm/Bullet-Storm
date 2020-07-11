using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject healthPickupEffect;

    void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.CompareTag("Player"))
       {
           Pickup(other);
       }
   }

   void Pickup(Collider2D player)
   {
       //Spawn effect
       Instantiate(healthPickupEffect, transform.position, transform.rotation);

       //Remove power up object
       Destroy(gameObject);
   }
}
