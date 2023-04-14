using Logic.Player;
using UnityEngine;

namespace Visualization
{
    [RequireComponent(typeof(Rigidbody))]
    public class IceBullet : MonoBehaviour
    {
        public GameObject explosionFX;
        public float damage;
        public float baseDamage;
        public float projectileSpeed;
        public float projectileBaseSpeed;
        private bool collided;
        private float lifetime;
        private float lifeTimer;
        private SphereCollider projectileCollider;
        public ParticleSystem beam;

        public IPlayerManager _playerManager;

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
            if (gameObject.tag == "PlayerProjectile")
            {
                projectileCollider.radius = 0.01f + _playerManager.GetAttack() * 0.0079f;
               ParticleSystem.MainModule main = beam.main;
                main.startSize = 0.1f + _playerManager.GetAttack() * 0.049f;
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
            if (gameObject.tag == "PlayerProjectile" && collision.gameObject.tag != "PlayerProjectile" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
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
