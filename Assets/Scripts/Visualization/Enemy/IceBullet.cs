using Logic.Player;
using UnityEngine;

namespace Visualization
{
    [RequireComponent(typeof(Rigidbody))]
    public class IceBullet : Projectile
    {
        public GameObject explosionFX;
        //public GameObject trailFX;
        private bool collided;
        private float lifetime;
        private float lifeTimer;
        private SphereCollider projectileCollider;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();

            if (gameObject.tag == "IceBullet")
            {
                damage = baseDamage + _playerManager.GetAttack() * 0.1f;
                projectileSpeed = projectileBaseSpeed - _playerManager.GetAttack() * 0.35f;
            }
            else
            {
                damage = baseDamage;
                projectileSpeed = projectileBaseSpeed;
            }

            projectileCollider = GetComponent<SphereCollider>();

            lifeTimer = 0;
            lifetime = 7f; // Projectile lives for 7 seconds if does not collide
        }

        private void Start()
        {
            if (gameObject.tag == "IceBullet")
            {
                projectileCollider.radius = 0.01f + _playerManager.GetAttack() * 0.0079f;
                //Instantiate(trailFX, transform.position, Quaternion.identity);
                
            }
        }

        private void Update()
        {
            lifeTimer += Time.deltaTime;

            if (lifeTimer > lifetime)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Player projectile collisions
            if (gameObject.tag == "IceBullet" && collision.gameObject.tag != "IceBullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;
                GameObject impact = Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
               
                
                FindObjectOfType<AudioManager>().Play("OnHit");
                

                Destroy(gameObject);
            }

            // Enemy projectile collisions
            if (gameObject.tag == "EnemyProjectile" && collision.gameObject.tag != "EnemyProjectile" && collision.gameObject.tag != "Enemy")
            {
                collided = true;

                GameObject impact = Instantiate(explosionFX, collision.contacts[0].point, Quaternion.identity);
                
                FindObjectOfType<AudioManager>().Play("OnHit");
                

                Destroy(gameObject);
            }
        }
    }
}
