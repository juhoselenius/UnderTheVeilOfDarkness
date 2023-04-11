using UnityEngine;

namespace Visualization
{
    public class Enemy : MonoBehaviour
    {
        public float maxHealth;
        [SerializeField] private float health;
        public float meleeDamage;

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
            Destroy(gameObject);
        }
    }
}
