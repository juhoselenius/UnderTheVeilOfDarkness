using Logic.Player;
using UnityEngine;

namespace Visualization
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public GameObject impactVFX;
        public GameObject trailFX;
        public float damage;
        public float baseDamage;
        public float projectileSpeed;
        public float projectileBaseSpeed;

        private bool collided;
        public ParticleSystem beam;
        private float lifetime;
        private float lifeTimer;
        public Rigidbody rb;
        public IPlayerManager _playerManager;

        private float attack;

        public Transform originTransform;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();

            if (gameObject.tag == "PlayerProjectile")
            {
                //damage = baseDamage + _playerManager.GetAttack() * 0.1f;
                //projectileSpeed = projectileBaseSpeed - _playerManager.GetAttack() * 0.35f;
                damage = baseDamage;
                projectileSpeed = projectileBaseSpeed;
            }
            else if (gameObject.tag == "IceBullet")
            {
                //damage = baseDamage + _playerManager.GetAttack() * 0.2f;
                //projectileSpeed = projectileBaseSpeed - _playerManager.GetAttack() * 0.45f;
                damage = baseDamage;
                projectileSpeed = projectileBaseSpeed;
            }
            else if (gameObject.tag == "FireBullet")
            {
                damage = baseDamage;
                projectileSpeed = projectileBaseSpeed;
            }
            else
            {
                damage = baseDamage;
                projectileSpeed = projectileBaseSpeed;
            }

            rb = GetComponent<Rigidbody>();

            lifeTimer = 0;
            lifetime = 7f; // Projectile lives for 7 seconds if does not collide

            attack = _playerManager.GetAttack();
        }

        private void Update()
        {
            lifeTimer += Time.deltaTime;

            if (lifeTimer > lifetime)
            {
                if(attack != 0)
                {
                    GameObject impact = Instantiate(impactVFX, gameObject.transform.position, Quaternion.identity);
                    Destroy(impact, 2f);
                }
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Player projectile collisions
            if (gameObject.tag == "PlayerProjectile" && collision.gameObject.tag != "PlayerProjectile" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                }

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);

                Destroy(impact, 2f);

                Destroy(gameObject);
            }

            // Rock collisions
            if (gameObject.tag == "Rock" && collision.gameObject.tag != "Rock" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                }
            }

            if (gameObject.tag == "GreenProjectile" && collision.gameObject.tag != "GreenProjectile" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                }

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);

                Destroy(impact, 2f);

                Destroy(gameObject);
            }


            // Enemy projectile collisions
            if (gameObject.tag == "EnemyProjectile" && collision.gameObject.tag != "EnemyProjectile" && collision.gameObject.tag != "Enemy")
            {
                collided = true;

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);

                Debug.Log("EnemyProjectile collided with " + collision.gameObject.tag);

                Destroy(impact, 2f);

                Destroy(gameObject);
            }

            if (gameObject.tag == "IceBullet" && collision.gameObject.tag != "IceBullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                }

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);

                Destroy(impact, 1f);

                FindObjectOfType<AudioManager>().Play("OnHit");

                Destroy(gameObject);
            }

            if (gameObject.tag == "FireBullet" && collision.gameObject.tag != "FireBullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
                impact.transform.parent = collision.transform;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                    Destroy(impact, 4f);
                }
                else
                {
                    Destroy(impact, 1f);
                }

                FindObjectOfType<AudioManager>().Play("OnHit");

                Destroy(gameObject);
            }

            /*
            if (gameObject.tag == "StickyBullet" && collision.gameObject.tag != "StickyBullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerWeapon" && !collided)
            {
                collided = true;

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SmallSkeleton")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                    GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
                    Debug.Log("StickyBullet collided with " + collision.gameObject.tag);

                    FindObjectOfType<AudioManager>().Play("OnHit");

                    Destroy(impact, 2f);
                    Destroy(gameObject);
                }
                else if(collision.gameObject.tag == "EnemyProjectile")
                {
                    GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
                    Debug.Log("StickyBullet collided with " + collision.gameObject.tag);

                    FindObjectOfType<AudioManager>().Play("OnHit");

                    Destroy(impact, 2f);
                    Destroy(gameObject);
                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    Debug.Log("StickyProjectile stuck into " + collision.gameObject.tag);
                }
            }
            */
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.tag == "StickyBullet" && other.gameObject.tag != "StickyBullet" && other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerWeapon" && collided)
            {
                if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "SmallSkeletonEnemy")
                {
                    GameObject.FindGameObjectWithTag("Crosshair").GetComponent<HitmarkerUI>().SetHitmarker();
                    GameObject impact = Instantiate(impactVFX, transform.position, Quaternion.identity);
                    Debug.Log("EnemyProjectile collided with " + other.gameObject.tag);

                    FindObjectOfType<AudioManager>().Play("OnHit");

                    Destroy(impact, 2f);
                    Destroy(gameObject);
                }
            }
        }
    }
}

