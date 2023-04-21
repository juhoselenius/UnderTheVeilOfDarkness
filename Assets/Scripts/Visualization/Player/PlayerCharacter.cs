using Logic.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visualization
{
    public class PlayerCharacter : MonoBehaviour
    {
        public IPlayerManager _playerManager;
        public float currentPresetCooldown;
        public float maxPresetCooldown;      
        private float defense;
        

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentPresetCooldown = 0;         
            
        }

        private void Update()
        {
            if(currentPresetCooldown > 0)
            {
                currentPresetCooldown -= Time.deltaTime;
            }
            else
            {
                currentPresetCooldown = 0;
                if (Input.GetKeyDown("1"))
                {
                    _playerManager.ChangePreset(0);
                    currentPresetCooldown = maxPresetCooldown;
                }
                else if (Input.GetKeyDown("2"))
                {
                    _playerManager.ChangePreset(1);
                    currentPresetCooldown = maxPresetCooldown;
                }
                else if (Input.GetKeyDown("3"))
                {
                    _playerManager.ChangePreset(2);
                    currentPresetCooldown = maxPresetCooldown;
                }
            }
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

        private void OnTriggerEnter(Collider other)
        {
            // Player takes damage from enemy projectiles
            if (other.gameObject.tag == "EnemyProjectile")
            {
                Debug.Log("Player got hit by enemy projectile");
                TakeDamage(other.gameObject.GetComponent<Projectile>().damage);          
            }
        }

        private void Die()
        {
            // Code for what happens when player dies
            Debug.Log("Player is dead!");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerManager.UpdateHealth(1000f);
            SceneManager.LoadScene("GameOver");
        }
    }
}