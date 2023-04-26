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
        public GameObject firedProjectile;
        public AudioSource audioSource;
        public AudioClip clip;
        //public GameObject muzzle;
        
        // Overloading variables
        public float overLoadMax;
        private float overLoadMin;
        public float currentOverLoad;
        public bool overLoaded;
        public GameObject[] projectiles;
        private float attack;
        public GameObject primaryWeapon;
        public GameObject hand;
        
        [SerializeField] private float cooldownTimeOverload;
        [SerializeField] private float cooldownTimeShooting;
        
        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            fireRate = baseFireRate + _playerManager.GetAttack() * 0.2f; // The fire rate factor
            attack = _playerManager.GetAttack();
            projectile = projectiles[(int)attack];

            chooseWeapon();
            
            overLoaded = false;
            overLoadMin = 0f;
            overLoadMax = 100f;
            currentOverLoad = 0f;
            cooldownTimeOverload = 5f;
            cooldownTimeShooting = 10f;
        }

        void Update()
        {
            if(Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                if(overLoaded)
                {
                    if(currentOverLoad > overLoadMin)
                    {
                        currentOverLoad -= Time.deltaTime * (overLoadMax / cooldownTimeOverload);
                    }
                    else
                    {
                        overLoaded = false;
                    }
                }
                else
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
            else
            {
                if(overLoaded)
                {
                    // Decreasing overload when overloaded and not pressing shoot
                    if(currentOverLoad > overLoadMin)
                    {
                        currentOverLoad -= Time.deltaTime * (overLoadMax / cooldownTimeOverload);
                    }
                    else
                    {
                        overLoaded = false;
                        currentOverLoad = 0f;
                    }
                }
                else
                {
                    // Decreasing overload when not overloaded and not pressing shoot
                    if (currentOverLoad > overLoadMin)
                    {
                        currentOverLoad -= Time.deltaTime * (overLoadMax / cooldownTimeShooting);
                    }
                    else
                    {
                        currentOverLoad = 0f;
                    }
                }
            }
        }

       public void Shoot()
        {
            if (overLoaded == false)
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
                
                InstantiateProjectile();
                playSound();
            }
        }

        void InstantiateProjectile()
        {
            if(projectile.tag == "Rock")
            {
                firedProjectile = Instantiate(projectile, hand.transform.position, Quaternion.identity);
            }
            else
            {
                firedProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
            }
            
            /*if(projectile.tag == "PlayerProjectile" || projectile.tag =="Bullet")
            {
                Instantiate(muzzle, projectileSpawn.position, projectileSpawn.rotation, projectileSpawn.transform);
            }*/

            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawn.position).normalized * projectileSpeed;

            // Increasing overload bar and checking if it filled up
            if(_playerManager.GetAttack() > 0)
            {
                // This makes the projectile wobble a little at the beginning of the flight path
                //iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
                
                currentOverLoad += 10 + _playerManager.GetAttack() * 5f;
            }

            if (currentOverLoad >= overLoadMax)
            {
                currentOverLoad = overLoadMax;
                overLoaded = true;
            }


            /*if (projectile.tag == "Bullet")
            {
                currentOverLoad += 15;
            }
            if (projectile.tag == "PlayerProjectile")
            {
                currentOverLoad += 20;
            }
            if (projectile.tag == "FireBullet")
            {
                currentOverLoad += 25;
            }

            if (projectile.tag == "IceBullet")
            {
                currentOverLoad += 30;
            }*/
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
            fireRate = baseFireRate + newValue * 0.15f;
            chooseWeapon();
            projectile = projectiles[(int)_playerManager.GetAttack()];
        }

        public void playSound()
        {

            audioSource.PlayOneShot(clip);
        }

        private void chooseWeapon()
        {
            attack = _playerManager.GetAttack();
            if (attack == 0)
            {
                hand.SetActive(true);
                primaryWeapon.SetActive(false);
            }
            else
            {
                primaryWeapon.SetActive(true);
                hand.SetActive(false);
                
            }
        }     
    }
}
