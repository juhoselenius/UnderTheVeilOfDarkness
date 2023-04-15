using Logic.Player;
using UnityEngine;

// https://www.youtube.com/watch?v=T5y7L1siFSY

namespace Visualization
{
    public class WeaponManager : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        public Camera playerCam;
        public Transform projectileSpawn;
        public GameObject projectile;
        public float fireRate;
        public float baseFireRate;
        public float arcRange;

        private Vector3 destination;
        private float timeToFire;
        
        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            fireRate = baseFireRate - _playerManager.GetAttack() * 0.05f;
        }

        void Update()
        {
            if(Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

        void Shoot()
        {
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                float distance = (hit.point - ray.GetPoint(0)).magnitude;
            
                if(distance > 2)
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

            InstantiateProjectile();
        }

        void InstantiateProjectile()
        {
            
            GameObject firedProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
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
            fireRate = baseFireRate - newValue * 0.05f;
        }
    }
}
