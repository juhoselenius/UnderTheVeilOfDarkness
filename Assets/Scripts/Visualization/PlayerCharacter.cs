using Logic.Player;
using UnityEngine;

namespace Visualization
{
    public class PlayerCharacter : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        private float defense;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                _playerManager.ChangePreset(0);
            }
            else if (Input.GetKeyDown("2"))
            {
                _playerManager.ChangePreset(1);
            }
            else if (Input.GetKeyDown("3"))
            {
                _playerManager.ChangePreset(2);
            }
        }

        private void TakeDamage(float amount)
        {
            defense = _playerManager.GetDefense();
            _playerManager.UpdateHealth(-amount + (defense * 0.005f)); // The defense reduction factor is 0.005 here
        }

        private void OnTriggerEnter(Collider other)
        {
            // Player takes damage from enemy projectiles
            if (other.tag == "EnemyProjectile")
            {
                TakeDamage(other.GetComponent<Projectile>().damage);
            }

            //Player takes damage from enemy melee
            if(other.tag == "Enemy")
            {
                TakeDamage(other.GetComponent<Enemy>().meleeDamage);
            }
            
            // Check if player dies
            if (_playerManager.GetHealth() <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Code for what happens when player dies
        }
    }
}