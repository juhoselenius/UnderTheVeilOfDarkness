using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visualization;

namespace Visualization
{
    public class WeaponTakeDamage : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        private float defense;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        public void TakeDamage(float amount)
        {
            defense = _playerManager.GetDefense();
            _playerManager.UpdateHealth(-amount * (1f - (defense * 0.005f))); // The defense reduction factor is 0.005 here

            if (_playerManager.GetHealth() <= 0)
            {
                Die();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Player takes damage from enemy projectiles
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                Debug.Log("Player got hit by enemy projectile");
                TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            }
        }

        private void Die()
        {
            // Code for what happens when player dies
            Debug.Log("Player is dead!");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
