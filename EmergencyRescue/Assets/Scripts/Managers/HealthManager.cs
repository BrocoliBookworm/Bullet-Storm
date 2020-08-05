using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int correctLayer;
    public SpriteRenderer spriteRend;

    void Start() 
    {
        CheckSprite();
    }

    void Update() 
    {   
       
    }

    public virtual void Die() 
    {
        
    }

    public void CheckSprite()
    {
        correctLayer = gameObject.layer;

        //Only gets the renderer on the parent object
        //Doesn't work for children such as the enemies
        spriteRend = GetComponent<SpriteRenderer>();
        
        if(spriteRend == null)
        {
            spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

            if(spriteRend == null)
            {
                Debug.Log("Object " + gameObject.name + " has no sprite renderer");
            }
        }
    }

    public virtual void HealthBoost()
    {

    }

    public virtual void Invulnerability()
    {

    }

    public virtual void TakeDamage(int damageTaken)
    {

    }
}
