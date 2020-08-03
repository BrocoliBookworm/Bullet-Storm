using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// defines how frequently and what damage the ray deals. It adds visual effect when the weapon power rises. 
/// When colliding with the ‘Player’ it sends the command: ‘receive damage’.
/// </summary>

//video effects for different stages of ray power
[System.Serializable]
public class RayVfx
{
    public GameObject stage2, stage3, stage4;
}

public class PlayerRay : MonoBehaviour {

    [Tooltip("how often 'Enemy' receives damage from the ray in seconds")]
    public float frequency;

    [Tooltip("ray power on the first stage")]
    public int damage;

    public RayVfx rayVfx;

    private void Start()
    {
        //updating video effect depending on ray power
        UpdateVfx();
    }

    //when colliding with the 'Enemy' sending the command 'receive damage'
    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.tag == "Enemy") 
        {
            collision.GetComponent<Enemy>().GetIndestructibleDamage(frequency, damage*PlayerShooting.instance.weaponPower); 
        }
    }

    //updating video effect depending on ray power
    public void UpdateVfx()
    {
        switch (PlayerShooting.instance.weaponPower)
        {
            case 1:
                rayVfx.stage2.SetActive(false);
                rayVfx.stage3.SetActive(false);
                rayVfx.stage4.SetActive(false);
                break;
            case 2:
                rayVfx.stage2.SetActive(true);
                rayVfx.stage3.SetActive(false);
                rayVfx.stage4.SetActive(false);
                break;
            case 3:
                rayVfx.stage2.SetActive(true);
                rayVfx.stage3.SetActive(true);
                rayVfx.stage4.SetActive(false);
                break;
            case 4:
                rayVfx.stage2.SetActive(true);
                rayVfx.stage3.SetActive(true);
                rayVfx.stage4.SetActive(true);
                break;
        }
    }
}
