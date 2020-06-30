using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform centralFirepoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            regularShoot();
        }
    }

    void regularShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, centralFirepoint.position, centralFirepoint.rotation); // Creates the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); //Access the bullets rigidbody2d component
        rb.AddForce(centralFirepoint.up * bulletForce,ForceMode2D.Impulse); // Puts force on the bullet
    }
}
