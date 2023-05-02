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
        public float initialEnemySpeed;
        public float enemyBaseSpeed;
        private float speedTimer;
        public Vector3 direction;
        private float collidedObjectDamage;
        public GameObject iceImpact;
        public Vector3 burnEffect;
        private float burnCoolDown;
        private bool burning;
        public float burningTime;
        private float burnTimer;
        private int burnTimes; // How many times burn damage is applied

        [SerializeField] private float cooldown = 5f;

        public event Action<bool> EnemyGotHit;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
            enemyBaseSpeed = 1f;
            initialEnemySpeed = enemyBaseSpeed;
            speedTimer = 0;
            burnCoolDown = 0.7f;
            burning = false;
            burnTimer = 0;
            burnTimes = 0;
            burningTime = 4f;
        }

        void Start()
        {
            direction = new Vector3(0, 0, 0);
            health = maxHealth;
            //iceImpact.SetActive(false);
        }

        private void Update()
        {
            // Defreezing the enemy (increasing the speed in seconds)
            if (enemyBaseSpeed < initialEnemySpeed)
            {
                speedTimer += Time.deltaTime;
                if (speedTimer > 1f)
                {
                    enemyBaseSpeed *= 1.25f;
                    speedTimer = 0;
                }
            }
            else
            {
                speedTimer = 0;
                enemyBaseSpeed = initialEnemySpeed;
               // iceImpact.SetActive(false);
            }
            /*
            if(burning)
            {
                burnTimer += Time.deltaTime;
                
                if(burnTimes < burningTime)
                {
                    // Burn timer is reset after every second, burn damage is applied and burn times counter increased
                    if(burnTimer > 1f)
                    {
                        health -= 5f;
                        burnTimer = 0;
                        burnTimes++;

                        if (health <= 0)
                        {
                            EnemyDie();
                        }
                    }
                }
                else
                {
                    //burning = false;
                    //burnTimer = 0;
                    //burnTimes = 0;
                }
            */
        }

        /*if (burning)
        {
            Burn();
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return;
            burning = false;
            cooldownTimer = burnCoolDown;
        }*/
    


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Enemy was hit");
            if (collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "StickyBullet" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "GreenProjectile")
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
                enemyBaseSpeed *= 0.75f;
                //iceImpact.SetActive(true);

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

                health -= collision.gameObject.GetComponent<Projectile>().damage;
                //collidedObjectDamage = collision.gameObject.GetComponent<Projectile>().damage;
                StartCoroutine(KnockBack());
                //burning = true;
                
                // Resetting burn counters on every hit
                //burnTimer = 0;
                //burnTimes = 0;
            }

            if (health <= 0)
            {
                EnemyDie();
            }
        }

        private void EnemyDie()
        {
            // Here code for what happens when the enemy dies
            _gameManager.DecreaseLevel2EnemiesLeft();
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

        /*private void Burn()
        {
            //health -= (collidedObjectDamage / 30);
            var impactFire = Instantiate(impact, transform.position, Quaternion.identity);
            new WaitForSeconds(0.3f);
            health -= (collidedObjectDamage / 2f);
            Destroy(impactFire);
            Debug.Log("Health: " + health);

        }*/
    }
}

