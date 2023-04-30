using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class InvisibleEnemy : MonoBehaviour
    {
        public GameObject enemyProjectilePrefab;
        public float fireRateTimer;
        public float fireRate; // The pause between shots in seconds
        public Transform projectileSpawn;
        public float arcRange;

        public GameObject ground;
        public GameObject body;
        public float positionHeight;
        public AudioSource audioSource;
        public AudioSource audioSourceidle;
        public AudioClip clip;
        public GameObject soundText;
        public AudioClip clipidle;
        public float soundtimer = 10f;
        public float nextsound;

        private IPlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        void Start()
        {
            Vector3 groundPosition = ground.transform.position;
            body.GetComponent<Transform>().position = new Vector3(groundPosition.x, groundPosition.y + positionHeight, groundPosition.z);

            fireRateTimer = fireRate - 0.01f;
        }

        void Update()
        {
            if (Time.time  > nextsound)
            {
                playidlesound();
                 nextsound = Time.time + soundtimer;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            // The player enters the sensing area of the enemy and enemy shoots
            if (other.CompareTag("Player"))
            {
                fireRateTimer += Time.deltaTime;

                if (fireRateTimer > fireRate)
                {
                    fireRateTimer = 0;
                    Vector3 playerPosition = other.transform.position;
                    Shoot(playerPosition);
                }
            }
        }

        void Shoot(Vector3 targetPosition)
        {
            playSound();
            GameObject firedProjectile = Instantiate(enemyProjectilePrefab, projectileSpawn.position, Quaternion.identity);
            firedProjectile.GetComponent<Projectile>().originTransform = projectileSpawn.transform;
            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (targetPosition - projectileSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
        }
        public void playSound()
        {
            audioSource.PlayOneShot(clip);

            if (_playerManager.GetHearing() == 2f)
            {
                soundText.GetComponent<SoundToText>().textSound = "Pew";
                Instantiate(soundText, body.transform.position, Quaternion.identity);
            }
        }


        public void playidlesound()
        {
            audioSourceidle.PlayOneShot(clipidle);
        }
    }
}
