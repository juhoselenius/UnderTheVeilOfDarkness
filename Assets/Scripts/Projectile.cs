using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=T5y7L1siFSY

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    public float damage;
    
    private bool collided;
    private float lifetime;
    private float lifeTimer;

    private void Awake()
    {
        lifeTimer = 0;
        lifetime = 7f;
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        
        if(lifeTimer > lifetime)
        {
            Destroy(gameObject);
        }
    }

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
