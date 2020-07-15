using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;

    //public Vector2 movementDirection;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        FaceMouse();
    }

    void FixedUpdate()
    {   
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2d.velocity = targetVelocity * speed;
     }
 
    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
