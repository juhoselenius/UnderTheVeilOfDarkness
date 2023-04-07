using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    [SerializeField]
    private float health;
    
    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy was hit");
        if(collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<Projectile>().damage;
        }
    }
}
