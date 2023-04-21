using Logic.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Visualization
{
    public class PlayerCharacter : MonoBehaviour
    {
        public IPlayerManager _playerManager;
        
        public float currentPresetCooldown;
        public float maxPresetCooldown;

        public float currentHearingCooldown;
        public float maxHearingCooldown;
        public GameObject xRayCamera;
        public float knockBackForce;
        private Vector3 direction;
        private float defense;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentPresetCooldown = 0;
            currentHearingCooldown = 0;
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

            // Activating X-Ray camera with "R"
            if (currentHearingCooldown > 0)
            {
                currentHearingCooldown -= Time.deltaTime;
            }
            else
            {
                currentHearingCooldown = 0;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if(_playerManager.GetHearing() >= 3f)
                    {
                        if(xRayCamera.activeInHierarchy)
                        {
                            xRayCamera.SetActive(false);
                        }
                        else
                        {
                            xRayCamera.SetActive(true);
                        }
                        currentHearingCooldown = maxHearingCooldown;
                    }
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
                direction = (GameObject.FindGameObjectWithTag("Player").transform.position - other.gameObject.transform.position).normalized;
                Debug.Log("Direction PC: " + direction);
                StartCoroutine(knockBack());
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

        IEnumerator knockBack()
        {
            float knockBacktime = Time.time;
            while (Time.time < knockBacktime + 0.2f)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position += (direction * Time.deltaTime * knockBackForce);
                Debug.Log("Pelaajan transform: " + direction);
                yield return null;
            }

        }
    }
}