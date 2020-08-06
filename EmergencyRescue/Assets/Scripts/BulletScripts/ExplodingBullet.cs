using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : Bullet
{
    public GameObject explosionEffect;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            ExplodeDestroy();
        }
    }

    public override void ExplodeDestroy()
    {
        var clone = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(clone, 1f);
        Destroy(gameObject);
    }
}
