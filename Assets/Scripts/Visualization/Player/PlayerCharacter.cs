using Logic.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace Visualization
{
    public class PlayerCharacter : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        public float knockBackForce;
        private Vector3 direction;
        private float defense;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        private void Update()
        {
            
        }

        public void TakeDamage(float amount)
        {
            //defense = _playerManager.GetDefense();
            //_playerManager.UpdateHealth(-amount * (1f - (defense * 0.005f))); // The defense reduction factor is 0.005 here
            _playerManager.UpdateHealth(-amount);

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
                //Debug.Log("Direction PC: " + direction);
                StartCoroutine(KnockBack());
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

        IEnumerator KnockBack()
        {
            float knockBacktime = Time.time;
            while (Time.time < knockBacktime + 0.2f)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position += (direction * Time.deltaTime * knockBackForce);
                yield return null;
            }
        }
    }
}