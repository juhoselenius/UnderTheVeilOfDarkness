using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visualization;

public class Shield : MonoBehaviour
{
    public float maxLifetime;
    public float lifetime;
    public float maxHealth;
    public float health;
    
    void Start()
    {
        lifetime = maxLifetime;
        health = maxHealth;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            ShieldDestroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyProjectile")
        {
            health -= collision.gameObject.GetComponent<Projectile>().damage;
        }
    }

    public void ShieldDestroy()
    {
        // Disable the collider
        // Code here for the shield polygons to fade out and float to the direction of the normal vector
        Destroy(gameObject);
    }
}
