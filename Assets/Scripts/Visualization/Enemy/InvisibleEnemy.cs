using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class InvisibleEnemy : MonoBehaviour
    {
        public GameObject enemyProjectilePrefab;
        public float fireRateTimer;
        public float fireRate; // The pause between shots in seconds
        public Transform projectileSpawn;
        public float arcRange;

        public GameObject ground;
        public GameObject body;
        public float positionHeight;

        // Start is called before the first frame update
        void Start()
        {
            Vector3 groundPosition = ground.transform.position;
            body.GetComponent<Transform>().position = new Vector3(groundPosition.x, groundPosition.y + positionHeight, groundPosition.z);

            fireRateTimer = fireRate - 0.01f;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerStay(Collider other)
        {
            // The player enters the sensing area of the enemy and enemy shoots
            if (other.CompareTag("Player"))
            {
                fireRateTimer += Time.deltaTime;

                if (fireRateTimer > fireRate)
                {
                    fireRateTimer = 0;
                    Vector3 playerPosition = other.transform.position;
                    Shoot(playerPosition);
                }

            }
        }

        void Shoot(Vector3 targetPosition)
        {
            GameObject firedProjectile = GameObject.Instantiate(enemyProjectilePrefab, projectileSpawn.position, Quaternion.identity);
            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (targetPosition - projectileSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
        }
    }
}
