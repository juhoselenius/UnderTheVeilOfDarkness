using Logic.Game;
using Logic.Player;
using UnityEngine;

// https://www.youtube.com/watch?v=T5y7L1siFSY

namespace Visualization
{
    public class WeaponManager : MonoBehaviour
    {
        public IPlayerManager _playerManager;
        private IGameManager _gameManager;

        public Camera playerCam;
        public Transform projectileSpawn;
        public Transform projectileSpawnGrim;
        public Transform projectileSpawnMauler;
        public Transform projectileSpawnMauler2;
        public Transform projectileSpawnMauler3;
        public Transform projectileSpawnFS;
        public Transform projectileSpawnPistol;
        
        public GameObject projectile;
        public float fireRate;
        public float baseFireRate;
        public float arcRange;
        private Vector3 destination;
        private float timeToFire;
        public GameObject firedProjectile;
        public AudioSource audioSource;
        public AudioClip clip;
        public AudioClip cliprock;
        
        // Overloading variables
        public float overLoadMax;
        private float overLoadMin;
        public float currentOverLoad;
        public bool overLoaded;
        public GameObject[] projectiles;
        private float attack;
        public GameObject primaryWeapon;
        public GameObject hand;
        public GameObject mauler;
        public GameObject grimBrand;
        public GameObject fireSleet;
        public GameObject pistol;
        private float projectileSpeed;
        
        [SerializeField] private float cooldownTimeOverload;
        [SerializeField] private float cooldownTimeShooting;

        private bool paused;
        
        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            _gameManager = ServiceLocator.GetService<IGameManager>();

            //fireRate = baseFireRate + _playerManager.GetAttack() * 0.2f; // The fire rate factor
            fireRate = baseFireRate;
            attack = _playerManager.GetAttack();
            projectile = projectiles[(int)attack];

            ChooseWeapon();
            
            overLoaded = false;
            overLoadMin = 0f;
            overLoadMax = 100f;
            currentOverLoad = 0f;
            cooldownTimeOverload = 5f;
            cooldownTimeShooting = 10f;

            paused = false;
        }

        void Update()
        {
            if(Input.GetButton("Fire1") && Time.time >= timeToFire && !paused)
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
                if (attack == 0)
                {
                    playSoundrock();
                }
                else
                {
                    playSound();
                }
            }
        }

        void InstantiateProjectile()
        {
            
            if (projectile.tag == "Rock")
            {
                firedProjectile = Instantiate(projectile, hand.transform.position, Quaternion.identity);
                projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                firedProjectile.GetComponent<Rigidbody>().velocity = (destination - hand.transform.position).normalized * projectileSpeed;
            }
            /*
            else if(projectile.tag == "StickyBullet")
            {
                firedProjectile = Instantiate(projectile, projectileSpawnGrim.position, Quaternion.identity);
                projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawnGrim.position).normalized * projectileSpeed;
            }
            */

            else if(projectile.tag == "GreenProjectile")
            {
                firedProjectile = Instantiate(projectile, projectileSpawnGrim.position, Quaternion.identity);
                projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawnGrim.position).normalized * projectileSpeed;
            }

            else if (projectile.tag == "PlayerProjectile")
            {
                firedProjectile = Instantiate(projectile, projectileSpawnPistol.position, Quaternion.identity);
                projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawnPistol.position).normalized * projectileSpeed;
            }
            else if (projectile.tag == "FireBullet")
            {
                
                for (int i = 0; i < 3; i++)
                {
                    if(i == 0)
                    {
                        firedProjectile = Instantiate(projectile, projectileSpawnMauler.position, Quaternion.identity);
                    }
                    else if (i == 1)
                    {
                        firedProjectile = Instantiate(projectile, projectileSpawnMauler2.position, Quaternion.identity);
                    }
                    if (i == 2)
                    {
                        firedProjectile = Instantiate(projectile, projectileSpawnMauler3.position, Quaternion.identity);
                    }                  
                    projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                    firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawnMauler.position).normalized * projectileSpeed;
                    iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 1f));
                }
            }
            else if (projectile.tag == "IceBullet")
            {
                firedProjectile = Instantiate(projectile, projectileSpawnFS.position, Quaternion.identity);
                projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
                firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawnFS.position).normalized * projectileSpeed;
            }
 
            // Increasing overload bar and checking if it filled up
            if(_playerManager.GetAttack() > 0)
            {
                // This makes the projectile wobble a little at the beginning of the flight path
                //iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 1f));
                
                //currentOverLoad += 10 + _playerManager.GetAttack() * 5f;

                // Add overload to the overload bar according to the used weapon
                switch(_playerManager.GetAttack())
                {
                    case 1:
                        currentOverLoad += 30f;
                        break;
                    case 2:
                        currentOverLoad += 18f;
                        break;
                    case 3:
                        currentOverLoad += 28f;
                        break;
                    case 4:
                        currentOverLoad += 7.5f;
                        break;
                    default:
                        break;
                }
            }

            if (currentOverLoad >= overLoadMax)
            {
                currentOverLoad = overLoadMax;
                overLoaded = true;
            }
        }

        private void OnEnable()
        {
            _playerManager.AttackChanged += ChangeFireRate;
            ChangeFireRate(_playerManager.GetAttack());

            _gameManager.GamePaused += PauseKeys;
            PauseKeys(_gameManager.GetGamePaused());
        }

        private void OnDisable()
        {
            _playerManager.AttackChanged -= ChangeFireRate;
        }

        void ChangeFireRate(float newValue)
        {
            //fireRate = baseFireRate + newValue * 0.15f;
            ChooseWeapon();
            projectile = projectiles[(int)_playerManager.GetAttack()];

            // Change weapon overload cooldown time
            switch (_playerManager.GetAttack())
            {
                case 0:
                    cooldownTimeOverload = 0.1f;
                    break;
                case 1:
                    cooldownTimeOverload = 1.2f;
                    break;
                case 2:
                    cooldownTimeOverload = 2.1f;
                    break;
                case 3:
                    cooldownTimeOverload = 3.2f;
                    break;
                case 4:
                    cooldownTimeOverload = 3.7f;
                    break;
                default:
                    break;
            }
        }

        public void playSound()
        {
            audioSource.PlayOneShot(clip);
        }

        private void ChooseWeapon()
        {
            attack = _playerManager.GetAttack();
            if (attack == 0)
            {
                hand.SetActive(true);
                grimBrand.SetActive(false);
                primaryWeapon.SetActive(false);
                mauler.SetActive(false);
                fireSleet.SetActive(false);
                pistol.SetActive(false);
                fireRate = 1f;
            }
            else if (attack == 1)
            {
                grimBrand.SetActive(true);
                hand.SetActive(false);
                primaryWeapon.SetActive(false);
                mauler.SetActive(false);
                fireSleet.SetActive(false);
                pistol.SetActive(false);
                fireRate = 1.5f;
            }        
            else if (attack == 2)
             {
                pistol.SetActive(true);
                primaryWeapon.SetActive(false);
                grimBrand.SetActive(false);
                mauler.SetActive(false);
                fireSleet.SetActive(false);
                hand.SetActive(false);
                fireRate = 2.25f;
            }
            else if (attack == 3)
            {             
                primaryWeapon.SetActive(true);
                mauler.SetActive(false);
                fireSleet.SetActive(false);
                hand.SetActive(false);
                grimBrand.SetActive(false);
                pistol.SetActive(false);
                fireRate = 1.25f;
            }
            else if (attack == 4)
            {
                fireSleet.SetActive(true);
                mauler.SetActive(false);
                hand.SetActive(false);
                grimBrand.SetActive(false);
                primaryWeapon.SetActive(false);
                pistol.SetActive(false);
                fireRate = 4f;
            }
        }

        private void PauseKeys(bool isGamePaused)
        {
            paused = isGamePaused;
        }

        public void playSoundrock()
        {
            audioSource.PlayOneShot(cliprock);
        }
    }
}
