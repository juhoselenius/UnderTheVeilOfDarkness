using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float force;
    private float timer;
    public GameObject impactVFX;
    private bool collided;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector3(direction.x, direction.y, direction.z).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
           
            if (collision.gameObject.tag != "EnemyProjectile" && collision.gameObject.tag != "Enemy" )
            {
                collided = true;

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
                Destroy(impact, 2f);

                Destroy(gameObject);
            }
        
    }
}
