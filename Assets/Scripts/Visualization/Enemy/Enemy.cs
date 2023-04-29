using Logic.Game;
using System;
using System.Collections;
using UnityEngine;

namespace Visualization
{
    public class Enemy : MonoBehaviour
    {
        private IGameManager _gameManager;

        public GameObject enemyPrefab;

        public float maxHealth;
        [SerializeField] private float health;
        public float meleeDamage;

        public float cooldownTimer;
        public float knockBackForce;
        public float brake;
        public float enemyBaseSpeed;
        public Vector3 direction;
        private float collidedObjectDamage;
        public GameObject impact;
        public Vector3 burnEffect;
        private float burnCoolDown;

        [SerializeField] private float cooldown = 5f;

        public event Action<bool> EnemyGotHit;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
            enemyBaseSpeed = 1f;
            burnCoolDown = 0.5f;
        }

        void Start()
        {
            direction = new Vector3(0, 0, 0);
            health = maxHealth;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Enemy was hit");
            if (collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "StickyBullet" || collision.gameObject.tag == "Rock")
            {
                EnemyGotHit?.Invoke(true);
                health -= collision.gameObject.GetComponent<Projectile>().damage;
                direction = (transform.position - collision.transform.position).normalized;
                StartCoroutine(KnockBack());

            }
            else if (collision.gameObject.tag == "IceBullet")
            {
                EnemyGotHit?.Invoke(true);
                health -= collision.gameObject.GetComponent<Projectile>().damage;
                direction = (transform.position - collision.transform.position).normalized;
                StartCoroutine(KnockBack());
                enemyBaseSpeed -= 0.25f;

                /*
                rb.isKinematic = true;
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer > 0) return;
                cooldownTimer = cooldown;
                rb.isKinematic = false;
                */

            }
            else if (collision.gameObject.tag == "FireBullet")
            {
                EnemyGotHit?.Invoke(true);
                direction = (transform.position - collision.transform.position).normalized;

                collidedObjectDamage = collision.gameObject.GetComponent<Projectile>().damage;
                //StartCoroutine(KnockBack());
                Burn();
            }

            if (health <= 0)
            {
                EnemyDie();
            }
        }

        private void EnemyDie()
        {
            // Here code for what happens when the enemy dies
            _gameManager.SetLevel2EnemiesLeft();
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            FindObjectOfType<AudioManager>().StopPlay("EnemyWalk");
            Destroy(enemyPrefab);
        }
        IEnumerator KnockBack()
        {
            float knockBacktime = Time.time;
            while (Time.time < knockBacktime + 0.2f)
            {
                transform.position += (direction * Time.deltaTime * knockBackForce);
                yield return null;
            }
        }

        private void Burn()
        {
            float burntime = Time.time;

            cooldownTimer -= Time.deltaTime;
            health -= (collidedObjectDamage / 30);
            Instantiate(impact, transform.position, Quaternion.identity);
            if (cooldownTimer > 0) return;
            cooldownTimer = burnCoolDown;
        }
    }
}

