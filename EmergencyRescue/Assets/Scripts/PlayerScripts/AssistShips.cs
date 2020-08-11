using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistShips : PlayerShooter
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Reload();

        if(Input.GetButton("Fire1"))
        {
            if(shotAvailable)
            {
                AssistShot();
                shotAvailable = false;
            }
        }
    }

    public override void AssistShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, centralFirepoint.position, centralFirepoint.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.AddForce(centralFirepoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
