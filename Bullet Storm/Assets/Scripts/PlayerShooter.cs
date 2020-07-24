using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    #pragma strict 
    // Start is called before the first frame update

    public Transform centralFirepoint;
    public GameObject bulletPrefab;
    public GameObject tripleBulletPrefab;
    public float bulletForce;
    public float tripleBulletForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(GameManager.Instance().survivors < 10)
            {
                RegularShoot();
                Debug.Log("regular shot");
            }
            else if(GameManager.Instance().survivors >= 10 && GameManager.Instance().survivors < 20)
            {
                TripleShot();
                Debug.Log("triple shot");
            }
            else if(GameManager.Instance().survivors >= 20)
            {
                Debug.Log("Exploding shot");
            }
        }
    }

    void RegularShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, centralFirepoint.position, centralFirepoint.rotation); // Creates the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); //Access the bullets rigidbody2d component
        rb.AddForce(centralFirepoint.up * bulletForce, ForceMode2D.Impulse); // Puts force on the bullet
    }

    void TripleShot()
    {
        GameObject tripleBullet = Instantiate(tripleBulletPrefab, centralFirepoint.position, centralFirepoint.rotation);
        Rigidbody2D[] rb = tripleBullet.GetComponentsInChildren<Rigidbody2D>();
        for(int i = 0; i < 3; i++)
        {
            rb[i].AddForce(centralFirepoint.up * tripleBulletForce, ForceMode2D.Impulse);
        }
    }
}
