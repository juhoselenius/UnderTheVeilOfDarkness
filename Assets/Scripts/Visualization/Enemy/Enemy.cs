using Logic.Game;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] private float cooldown = 5f;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
            enemyBaseSpeed = 1f;
        }

        void Start()
        {
            direction = new Vector3(0,0,0); 
            health = maxHealth;
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Enemy was hit");
            if(collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag =="Bullet" || collision.gameObject.tag == "FireBullet" || collision.gameObject.tag == "Rock")
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;                                        
                direction = (transform.position - collision.transform.position).normalized;
                StartCoroutine(knockBack());
                
            }
            else if(collision.gameObject.tag == "IceBullet")
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;
                direction = (transform.position - collision.transform.position).normalized;
                StartCoroutine(knockBack());
                enemyBaseSpeed -= 0.25f;
                
                /*
                rb.isKinematic = true;
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer > 0) return;
                cooldownTimer = cooldown;
                rb.isKinematic = false;
                */
                
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
        IEnumerator knockBack()
        {
           float knockBacktime = Time.time;
            while (Time.time < knockBacktime + 0.2f)
            {
                transform.position += (direction * Time.deltaTime * knockBackForce);             
                yield return null;
            }
        }
    }
}
