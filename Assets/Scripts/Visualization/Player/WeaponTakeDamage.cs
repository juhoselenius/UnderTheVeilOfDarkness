using Logic.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visualization
{
    public class WeaponTakeDamage : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        private float defense;
        public float knockBackForce;
        private Vector3 direction;
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
                direction = (GameObject.FindGameObjectWithTag("Player").transform.position - collision.transform.position).normalized;
                Debug.Log("Direction weaponTD: " + direction);
                StartCoroutine(knockBack());
            }
        }

        IEnumerator knockBack()
        {
            float knockBacktime = Time.time;
            while (Time.time < knockBacktime + 0.2f)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position += (direction * Time.deltaTime * knockBackForce);
                //transform.position += (direction * Time.deltaTime * knockBackForce);
                yield return null;
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
