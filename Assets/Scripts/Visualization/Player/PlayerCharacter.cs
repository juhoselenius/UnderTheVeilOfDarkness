using Logic.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            //Debug.Log("Player Health: " + _playerManager.GetHealth());
        }

        public void TakeDamage(float amount)
        {
            defense = _playerManager.GetDefense();
            _playerManager.UpdateHealth(-amount + (defense * 0.005f)); // The defense reduction factor is 0.005 here

            if (_playerManager.GetHealth() <= 0)
            {
                Die();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Player takes damage from enemy projectiles
            if (other.gameObject.tag == "EnemyProjectile")
            {
                Debug.Log("Player got hit by enemy projectile");
                TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
            }
        }

        // Melee doesn't work!!!
        private void OnCollisionEnter(Collision collision)
        {
            //Player takes damage from enemy melee
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Player got hit by melee");
                TakeDamage(collision.gameObject.GetComponent<Enemy>().meleeDamage);
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