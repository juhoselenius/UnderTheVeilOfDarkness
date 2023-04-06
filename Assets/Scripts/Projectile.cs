using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=T5y7L1siFSY

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    
    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
        {
            collided = true;

            GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
            Destroy(impact, 2f);

            Destroy(gameObject);
        }
    }
}
