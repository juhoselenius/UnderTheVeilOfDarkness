using Unity.VisualScripting;
using UnityEngine;

namespace Visualization
{
    public class Enemy : MonoBehaviour
    {
        public float maxHealth;
        [SerializeField] private float health;
        public float meleeDamage;
        public Rigidbody rb;
        public float cooldownTimer;
        [SerializeField] private float cooldown = 5f;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            health = maxHealth;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Enemy was hit");
            if(collision.gameObject.tag == "PlayerProjectile")
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;
            }
            else if(collision.gameObject.tag == "IceBullet")
            {
                health -= collision.gameObject.GetComponent<Projectile>().damage;
                rb.isKinematic = true;
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer > 0) return;
                cooldownTimer = cooldown;
                rb.isKinematic = false;
            }

            if (health <= 0)
            {
                EnemyDie();
            }
        }

        private void EnemyDie()
        {
            // Here code for what happens when the enemy dies
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            Destroy(gameObject);
        }
    }
}
