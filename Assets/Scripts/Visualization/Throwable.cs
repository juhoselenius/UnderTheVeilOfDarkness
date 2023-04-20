using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visualization;

namespace Visualization
{
    public class Throwable : MonoBehaviour
    {
        public float damage;
        public float baseDamage;
        public float projectileSpeed;
        public float projectileBaseSpeed;
        private float lifetime;
        private float lifeTimer;
        public float fireRate;
        public float baseFireRate;
        private float timeToFire;
        public Camera playerCam;
        private Vector3 destination;
        public GameObject thrownProjectile;
        public GameObject throwable;
        public float arcRange;
        public Transform throwableSpawn;
        private float attack;
        public GameObject weapon1;
        public GameObject weapon2;
        public GameObject hand;
        public IPlayerManager _playerManager;
        // Start is called before the first frame update

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            fireRate = baseFireRate + _playerManager.GetAttack() * 0.05f;
        }
        void Start()
        {
            damage = baseDamage;
            projectileSpeed = projectileBaseSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Throw();
                lifeTimer += Time.deltaTime;

                if (lifeTimer > lifetime)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Throw()
        {
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                float distance = (hit.point - ray.GetPoint(0)).magnitude;

                if (distance > 2)
                {
                    destination = hit.point;
                }
                else
                {
                    destination = ray.GetPoint(10);
                }
            }
            else
            {
                destination = ray.GetPoint(100);
            }

            InstantiateThrowable();

        }

        private void InstantiateThrowable()
        {
            Instantiate(throwable, throwableSpawn.position, Quaternion.identity);
            float projectileSpeed = thrownProjectile.GetComponent<Projectile>().projectileSpeed;
            thrownProjectile.GetComponent<Rigidbody>().velocity = (destination - throwableSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(thrownProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
        }

        private void OnEnable()
        {
            _playerManager.AttackChanged += ChangeFireRate;
            ChangeFireRate(_playerManager.GetAttack());
        }

        private void OnDisable()
        {
            _playerManager.AttackChanged -= ChangeFireRate;

        }

        void ChangeFireRate(float newValue)
        {
            fireRate = baseFireRate + newValue * 0.05f;
            chooseWeapon();


        }
        private void chooseWeapon()
        {
            attack = _playerManager.GetAttack();
            if (attack == 0)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                hand.SetActive(true);
            }
            else if (attack == 1 || attack == 2)
            {
                weapon2.SetActive(false);
                hand.SetActive(false);
                weapon1.SetActive(true);
                
            }
            else if (attack == 3 || attack == 4)
            {
                weapon1.SetActive(false);
                hand.SetActive(false);
                weapon2.SetActive(true);

            }
        }
    }
}
