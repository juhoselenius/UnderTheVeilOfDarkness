using Logic.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Visualization
{
    public class SmallEnemy : MonoBehaviour
    {
        private NavMeshAgent enemyNavMeshAgent;
        private SphereCollider sphereCollider;
        private int nextWaypoint;
        private float waitTimer;

        private float meleeTimer;

        public Transform playerCameraTransform; // Main Camera of the player has to be dragged here
        public PlayerCharacter player;
        public Path patrolPath;
        public bool playerInDetectionRange = false;
        public bool playerInAttackRange = false;
        public float walkSpeed;
        public float runSpeed;
        public float sightRange;
        public float patrolTurnWaitTime;
        public float meleeDamage;
        public float meleeRate;

        public GameObject enemyProjectilePrefab;
        public float fireRateTimer;
        public float fireRate; // The pause between shots in seconds
        public Transform projectileSpawn;
        public float arcRange;
        public AudioSource audioSource;
        public AudioClip clip;
        public GameObject soundTextShoot;
        public GameObject soundTextMove;
        public float soundTextOffset;
        public float soundTime;
        private float soundTimer;
        private bool hit;

        private IPlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            enemyNavMeshAgent = GetComponent<NavMeshAgent>();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = sightRange;
        }

        void FixedUpdate()
        {
            if (playerInDetectionRange)
            {
                if (playerInAttackRange)
                {
                    //animator.SetTrigger("Attack");
                    enemyNavMeshAgent.transform.LookAt(playerCameraTransform);

                    meleeTimer += Time.fixedDeltaTime;
                    if (meleeTimer > meleeRate)
                    {
                        meleeTimer = 0;

                        // Show damage indicator if damage comes from out of view
                        if (!DamageIndicatorUI.CheckIfObjectInSight(gameObject.transform))
                        {
                            DamageIndicatorUI.CreateIndicator(gameObject.transform);
                        }

                        player.TakeDamage(meleeDamage);
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer > fireRate)
                    {
                        fireRateTimer = 0;
                        Shoot(playerCameraTransform.position);
                    }

                    enemyNavMeshAgent.speed = runSpeed * gameObject.GetComponent<Enemy>().enemyBaseSpeed;
                    enemyNavMeshAgent.transform.LookAt(playerCameraTransform);
                    enemyNavMeshAgent.SetDestination(playerCameraTransform.position + new Vector3(0, 0, 1f));
                }
            }
            else if (hit)
            {
                enemyNavMeshAgent.speed = runSpeed * gameObject.GetComponent<Enemy>().enemyBaseSpeed * 2;
                enemyNavMeshAgent.transform.LookAt(playerCameraTransform);         
                enemyNavMeshAgent.SetDestination(playerCameraTransform.position + new Vector3(0, 0, 3f));
            }
            else
            {
                Patrol();
            }

            if (_playerManager.GetHearing() == 2f)
            {
                soundTimer += Time.deltaTime;
                if(soundTimer > soundTime)
                {
                    soundTimer = 0;
                    soundTextMove.GetComponent<SoundToText>().textSound = "Wob";
                    Instantiate(soundTextMove, gameObject.transform.position, Quaternion.identity);
                }
            }
            else
            {
                soundTimer = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInDetectionRange = true;
            }

            if (other.CompareTag("Rock") || other.CompareTag("StickyBullet") || other.CompareTag("PlayerProjectile") || other.CompareTag("FireBullet") || other.CompareTag("IceBullet"))
            {
                hit = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInDetectionRange = false;
            }
        }

        void Patrol()
        {
            enemyNavMeshAgent.destination = patrolPath.waypoints[nextWaypoint].position;
            enemyNavMeshAgent.isStopped = false;
            enemyNavMeshAgent.speed = walkSpeed;

            // Changing waypoint when current waypoint is reached through the NavMeshAgent
            if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer > patrolTurnWaitTime)
                {
                    // Looping through waypoints
                    nextWaypoint = (nextWaypoint + 1) % patrolPath.waypoints.Length;
                    waitTimer = 0;
                    enemyNavMeshAgent.speed = walkSpeed;
                }
            }
        }

        void Shoot(Vector3 targetPosition)
        {
            playSound();
            GameObject firedProjectile = GameObject.Instantiate(enemyProjectilePrefab, projectileSpawn.position, Quaternion.identity);
            firedProjectile.GetComponent<Projectile>().originTransform = gameObject.transform;
            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (targetPosition - projectileSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
        }

        public void playSound()
        {
            audioSource.PlayOneShot(clip);

            if (_playerManager.GetHearing() == 2f)
            {
                soundTextShoot.GetComponent<SoundToText>().textSound = "Pew";
                Vector3 newTextPosition = new Vector3(transform.position.x, transform.position.y + soundTextOffset, transform.position.z);
                Instantiate(soundTextShoot, newTextPosition, Quaternion.identity);
            }
        }
    }
}