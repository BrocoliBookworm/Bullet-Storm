﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the size of the ‘Boundary’ depending on Viewport. When objects go beyond the ‘Boundary’, they are destroyed or deactivated.
/// </summary>
public class Boundary : MonoBehaviour {

    BoxCollider2D boundareCollider;

    //receiving collider's component and changing boundary borders
    private void Start()
    {
        boundareCollider = GetComponent<BoxCollider2D>();
        ResizeCollider();
    }

    //changing the collider's size up to Viewport's size multiply 1.5
    void ResizeCollider() 
    {        
        Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        viewportSize.x *= 1.5f;
        viewportSize.y *= 1.5f;
        boundareCollider.size = viewportSize;
    }

    //when another object leaves collider
    private void OnTriggerExit2D(Collider2D collision) 
    {
        //if the object is projectile, destroying the object; if it's using pooling then deactivating it
        if (collision.tag == "Projectile")
        {
            if (collision.GetComponent<Projectile>().isPooled) 
                collision.gameObject.SetActive(false);
            else
                Destroy(collision.gameObject);
        }
        else if (collision.tag == "Rocket")
            collision.gameObject.SetActive(false);
        else if (collision.tag == "Bonus" || collision.tag == "Planet") 
            Destroy(collision.gameObject); 
    }

}
