using UnityEngine;
using Logic.Game;

namespace Visualization
{
    public class Enemy : MonoBehaviour
    {
        private IGameManager _gameManager;
        public float maxHealth;
        [SerializeField] private float health;
        public float meleeDamage;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();

        }
        void Start()
        {
            
            health = maxHealth;
        }

        

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Enemy was hit");
            if(collision.gameObject.tag == "PlayerProjectile")
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;
            }

            if (health <= 0)
            {
                EnemyDie();
            }
        }

        private void EnemyDie()
        {
            // Here code for what happens when the enemy dies
            _gameManager.SetallEnemiesCleared();
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            FindObjectOfType<AudioManager>().StopPlay("EnemyWalk");
            Destroy(gameObject);
        }
    }
}
